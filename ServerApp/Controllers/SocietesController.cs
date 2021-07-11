using AutoMapper;
using Data;
using Data.Societes;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.Societes;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocietesController : ControllerBase
    {
        private readonly ISocietesApiRepo _repository;
        private readonly IMapper _mapper;

        public SocietesController(ISocietesApiRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/SocietesApi
        [HttpGet]
        public ActionResult<IEnumerable<SocieteReadDto>> GetSociete()
        {
            var scociete = _repository.GetAllSocietes();
            return Ok(_mapper.Map<IEnumerable<SocieteReadDto>>(scociete));
        }

        // GET: api/SocietesApi/5
        [HttpGet("{id}", Name = "GetSociete")]
        public ActionResult<SocieteReadDto> GetSociete(string id)
        {
            var societe = _repository.GetSociete(id);

            if (societe == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SocieteReadDto>(societe));
        }

        [HttpPost]
        public ActionResult<SocieteReadDto> CreateSociete(SocieteCreateDto societeCreateDto)
        {
            var societeModel = _mapper.Map<Societe>(societeCreateDto);
            _repository.CreateSociete(societeModel);
            _repository.SaveChanges();
            var societeReadDto = _mapper.Map<SocieteReadDto>(societeModel);
            return CreatedAtRoute(nameof(GetSociete),
            new { Id = societeReadDto.AnneeDeb }, societeReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateSociete(string id, SocieteUpdateDto societeUpdateDto)
        {
            var societeModelFromRepo = _repository.GetSociete(id);
            if (societeModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(societeUpdateDto, societeModelFromRepo);
            _repository.UpdateSociete(societeModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialSocieteUpdate(string id, JsonPatchDocument<SocieteUpdateDto> patchDoc)
        {
            var societeModelFromRepo = _repository.GetSociete(id);
            if (societeModelFromRepo == null)
            {
                return NotFound();
            }
            var societeToPatch = _mapper.Map<SocieteUpdateDto>(societeModelFromRepo);
            patchDoc.ApplyTo(societeToPatch, ModelState);
            if (!TryValidateModel(societeToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(societeToPatch, societeModelFromRepo);
            _repository.UpdateSociete(societeModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSociete(string id)
        {
            var societeModelFromRepo = _repository.GetSociete(id);
            if (societeModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteSociete(societeModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

    }
}