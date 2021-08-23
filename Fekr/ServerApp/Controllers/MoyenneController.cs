using AutoMapper;
using Data.Moyenne;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.Moyenne;
using System.Collections.Generic;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoyenneController : ControllerBase
    {
        private readonly IMoyenneApiRepo _repository;
        private readonly IMapper _mapper;

        public MoyenneController(IMoyenneApiRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/MoyennesApi
        [HttpGet]
        public ActionResult<IEnumerable<MoyenneReadDto>> GetAllEspMoyennes()
        {
            var modules = _repository.GetAllMoyenne();
            return Ok(_mapper.Map<IEnumerable<MoyenneReadDto>>(modules));
        }

        // GET: api/MoyennesApi/5
        [HttpGet("{id}", Name = "GetMoyenne")]
        public ActionResult<MoyenneReadDto> GetMoyenne(string id)
        {
            var espMoyenne = _repository.GetMoyenneById(id);

            if (espMoyenne == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MoyenneReadDto>(espMoyenne));
        }

        [HttpPost]
        public ActionResult<MoyenneReadDto> CreateMoyenne(MoyenneCreateDto moduleCreateDto)
        {
            var moduleModel = _mapper.Map<AMoyenne>(moduleCreateDto);
            _repository.CreateMoyenne(moduleModel);
            _repository.SaveChanges();
            var moduleReadDto = _mapper.Map<MoyenneReadDto>(moduleModel);
            return CreatedAtRoute(nameof(GetMoyenne),
            new { Id = moduleReadDto.IdEt }, moduleReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateMoyenne(string id, MoyenneUpdateDto moduleUpdateDto)
        {
            var moduleModelFromRepo = _repository.GetMoyenneById(id);
            if (moduleModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(moduleUpdateDto, moduleModelFromRepo);
            _repository.UpdateMoyenne(moduleModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialMoyenneUpdate(string id, JsonPatchDocument<MoyenneUpdateDto> patchDoc)
        {
            var moduleModelFromRepo = _repository.GetMoyenneById(id);
            if (moduleModelFromRepo == null)
            {
                return NotFound();
            }
            var moduleToPatch = _mapper.Map<MoyenneUpdateDto>(moduleModelFromRepo);
            patchDoc.ApplyTo(moduleToPatch, ModelState);
            if (!TryValidateModel(moduleToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(moduleToPatch, moduleModelFromRepo);
            _repository.UpdateMoyenne(moduleModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMoyenne(string id)
        {
            var moduleModelFromRepo = _repository.GetMoyenneById(id);
            if (moduleModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteMoyenne(moduleModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}