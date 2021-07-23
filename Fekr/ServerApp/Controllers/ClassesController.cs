using AutoMapper;
using Data;
using Data.Classes;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.Classes;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassesApiRepo _repository;
        private readonly IMapper _mapper;

        public ClassesController(IClassesApiRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/ClassesApi
        [HttpGet]
        public ActionResult<IEnumerable<ClasseReadDto>> GetAllClasses()
        {
            var classes = _repository.GetAllClasses();
            return Ok(_mapper.Map<IEnumerable<ClasseReadDto>>(classes));
        }

        // GET: api/ClassesApi/5
        [HttpGet("{id}", Name = "GetClasse")]
        public ActionResult<ClasseReadDto> GetClasse(string id)
        {
            var classe = _repository.GetClasse(id);

            if (classe == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ClasseReadDto>(classe));
        }

        // POST: api/ClassesApi
        [HttpPost]
        public ActionResult<ClasseReadDto> CreateClasse(ClasseCreateDto classeCreateDto)
        {
            var classeModel = _mapper.Map<Classe>(classeCreateDto);
            _repository.CreateClasse(classeModel);
            _repository.SaveChanges();
            var classeReadDto = _mapper.Map<ClasseReadDto>(classeModel);
            return CreatedAtRoute(nameof(GetClasse),
            new { Id = classeReadDto.CodeCl }, classeReadDto);

        }

        [HttpPut("{id}")]
        public ActionResult UpdateClasse(string id, ClasseUpdateDto classeUpdateDto)
        {
            var classeModelFromRepo = _repository.GetClasse(id);
            if (classeModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(classeUpdateDto, classeModelFromRepo);
            _repository.UpdateClasse(classeModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialClasseUpdate(string id, JsonPatchDocument<ClasseUpdateDto> patchDoc)
        {
            var classeModelFromRepo = _repository.GetClasse(id);
            if (classeModelFromRepo == null)
            {
                return NotFound();
            }
            var classeToPatch = _mapper.Map<ClasseUpdateDto>(classeModelFromRepo);
            patchDoc.ApplyTo(classeToPatch, ModelState);
            if (!TryValidateModel(classeToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(classeToPatch, classeModelFromRepo);
            _repository.UpdateClasse(classeModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteClasse(string id)
        {
            var decidModelFromRepo = _repository.GetClasse(id);
            if (decidModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteClasse(decidModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}