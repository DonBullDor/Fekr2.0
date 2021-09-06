using AutoMapper;
using Data.ModuleEtudiant;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.ModuleEtudiant;
using System.Collections.Generic;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleEtudiantController : ControllerBase
    {

        private readonly IModuleEtudiant _repository;
        private readonly IMapper _mapper;

        public ModuleEtudiantController(IModuleEtudiant repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/ModulesApi
        [HttpGet]
        public ActionResult<IEnumerable<ModuleEtudiantReadDto>> GetAllModulesEtudiant()
        {
            var modulesEtudiant = _repository.GetAllModulesEtudiant();
            return Ok(_mapper.Map<IEnumerable<ModuleEtudiantReadDto>>(modulesEtudiant));
        }

        // GET: api/ModulesApi/5
        [HttpGet("{id}", Name = "GetModuleEtudiant")]
        public ActionResult<ModuleEtudiantReadDto> GetModuleEtudiantByID(string id)
        {
            var moduleEtudiant = _repository.GetModuleEtudiant(id);

            if (moduleEtudiant == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ModuleEtudiantReadDto>(moduleEtudiant));
        }

        [HttpPost]
        public ActionResult<ModuleEtudiantReadDto> CreateModuleEtudiant(ModuleEtudiantCreateDto ModuleEtudiantCreateDto)
        {
            var moduleEtudiantModel = _mapper.Map<EspModuleEtudiant>(ModuleEtudiantCreateDto);
            _repository.CreateModuleEtudiant(moduleEtudiantModel);
            _repository.SaveChanges();
            var moduleEtudiantReadDto = _mapper.Map<ModuleEtudiantReadDto>(moduleEtudiantModel);
            return CreatedAtRoute(nameof(GetModuleEtudiantByID),
            new { Id = moduleEtudiantReadDto.CodeCl }, moduleEtudiantReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateModuleEtudiant(string id, ModuleEtudiantUpdateDto moduleUpdateDto)
        {
            var moduleEtudiantModelFromRepo = _repository.GetModuleEtudiant(id);
            if (moduleEtudiantModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(moduleUpdateDto, moduleEtudiantModelFromRepo);
            _repository.UpdateModuleEtudiant(moduleEtudiantModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialModuleEtudiantUpdate(string id, JsonPatchDocument<ModuleEtudiantUpdateDto> patchDoc)
        {
            var moduleEtudiantModelFromRepo = _repository.GetModuleEtudiant(id);
            if (moduleEtudiantModelFromRepo == null)
            {
                return NotFound();
            }
            var moduleToPatch = _mapper.Map<ModuleEtudiantUpdateDto>(moduleEtudiantModelFromRepo);
            patchDoc.ApplyTo(moduleToPatch, ModelState);
            if (!TryValidateModel(moduleToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(moduleToPatch, moduleEtudiantModelFromRepo);
            _repository.UpdateModuleEtudiant(moduleEtudiantModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteModuleEtudiant(string id)
        {
            var moduleEtudiantModelFromRepo = _repository.GetModuleEtudiant(id);
            if (moduleEtudiantModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteModuleEtudiant(moduleEtudiantModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
