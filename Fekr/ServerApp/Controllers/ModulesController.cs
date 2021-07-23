using AutoMapper;
using Data;
using Data.Module;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.Modules;
using System.Collections.Generic;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleApiRepo _repository;
        private readonly IMapper _mapper;

        public ModulesController(IModuleApiRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/ModulesApi
        [HttpGet]
        public ActionResult<IEnumerable<ModuleReadDto>> GetAllEspModules()
        {
            var modules = _repository.GetAllModules();
            return Ok(_mapper.Map<IEnumerable<ModuleReadDto>>(modules));
        }

        // GET: api/ModulesApi/5
        [HttpGet("{id}", Name = "GetModule")]
        public ActionResult<ModuleReadDto> GetModule(string id)
        {
            var espModule = _repository.GetModule(id);

            if (espModule == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ModuleReadDto>(espModule));
        }

        [HttpPost]
        public ActionResult<ModuleReadDto> CreateModule(ModuleCreateDto moduleCreateDto)
        {
            var moduleModel = _mapper.Map<EspModule>(moduleCreateDto);
            _repository.CreateModule(moduleModel);
            _repository.SaveChanges();
            var moduleReadDto = _mapper.Map<ModuleReadDto>(moduleModel);
            return CreatedAtRoute(nameof(GetModule),
            new { Id = moduleReadDto.CodeModule }, moduleReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateModule(string id, ModuleUpdateDto moduleUpdateDto)
        {
            var moduleModelFromRepo = _repository.GetModule(id);
            if (moduleModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(moduleUpdateDto, moduleModelFromRepo);
            _repository.UpdateModule(moduleModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialModuleUpdate(string id, JsonPatchDocument<ModuleUpdateDto> patchDoc)
        {
            var moduleModelFromRepo = _repository.GetModule(id);
            if (moduleModelFromRepo == null)
            {
                return NotFound();
            }
            var moduleToPatch = _mapper.Map<ModuleUpdateDto>(moduleModelFromRepo);
            patchDoc.ApplyTo(moduleToPatch, ModelState);
            if (!TryValidateModel(moduleToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(moduleToPatch, moduleModelFromRepo);
            _repository.UpdateModule(moduleModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteModule(string id)
        {
            var moduleModelFromRepo = _repository.GetModule(id);
            if (moduleModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteModule(moduleModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}