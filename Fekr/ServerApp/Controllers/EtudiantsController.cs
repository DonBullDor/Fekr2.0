using System.Collections.Generic;
using AutoMapper;
using Data;
using Data.Etudiant;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.IConfiguration;
using Service.Repository;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtudiantsController : ControllerBase
    {
        private readonly ILogger<EtudiantsController> _logger;
        private readonly IMapper _mapper;
        private readonly IEtudiantApiRepo _repository;
        private IUnitOfWork _unitOfWork;

        public EtudiantsController(
            IEtudiantApiRepo repository,
            //IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            //_unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/EtudiantsApi
        [HttpGet]
        public ActionResult<IEnumerable<EtudiantReadDto>> GetAllEspEtudiants()
        {
            var etudiants = _repository.GetAllEtudiant();
            //var etudiants = _unitOfWork.Etudiants.All();
            return Ok(_mapper.Map<IEnumerable<EtudiantReadDto>>(etudiants));
            //return Ok(etudiants);
        }

        // GET: api/EtudiantsApi/5
        [HttpGet("{id}", Name = "GetEtudiant")]
        public ActionResult<EtudiantReadDto> GetEtudiant(string id)
        {
            var espEtudiant = _repository.GetEtudiant(id);
            if (espEtudiant == null) return NotFound();
            return Ok(_mapper.Map<EtudiantReadDto>(espEtudiant));
        }

        [HttpPost]
        public ActionResult<EtudiantReadDto> CreateEtudiant(EtudiantCreateDto etudiantCreateDto)
        {
            var etudiantModel = _mapper.Map<EspEtudiant>(etudiantCreateDto);
            _repository.CreateEtudiant(etudiantModel);
            _repository.SaveChanges();
            var etudiantReadDto = _mapper.Map<EtudiantReadDto>(etudiantModel);
            return CreatedAtRoute(nameof(GetEtudiant),
                new {Id = etudiantReadDto.IdEt}, etudiantReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateEtudiant(string id, EtudiantUpdateDto etudiantUpdateDto)
        {
            var etudiantModelFromRepo = _repository.GetEtudiant(id);
            if (etudiantModelFromRepo == null) return NotFound();
            _mapper.Map(etudiantUpdateDto, etudiantModelFromRepo);
            _repository.UpdateEtudiant(etudiantModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialEtudiantUpdate(string id, JsonPatchDocument<EtudiantUpdateDto> patchDoc)
        {
            var etudiantModelFromRepo = _repository.GetEtudiant(id);
            if (etudiantModelFromRepo == null) return NotFound();
            var etudiantToPatch = _mapper.Map<EtudiantUpdateDto>(etudiantModelFromRepo);
            patchDoc.ApplyTo(etudiantToPatch, ModelState);
            if (!TryValidateModel(etudiantToPatch)) return ValidationProblem(ModelState);
            _mapper.Map(etudiantToPatch, etudiantModelFromRepo);
            _repository.UpdateEtudiant(etudiantModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteEtudiant(string id)
        {
            var etudiantModelFromRepo = _repository.GetEtudiant(id);
            if (etudiantModelFromRepo == null) return NotFound();
            _repository.DeleteEtudiant(etudiantModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}