using AutoMapper;
using Data.PlanEtude;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.Plan_etude;
using System.Collections.Generic;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanEtudeController : ControllerBase
    {
        private readonly IPlanEtudeApiRepo _repository;
        private readonly IMapper _mapper;

        public PlanEtudeController(IPlanEtudeApiRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EspModulePanierClasseSaiso>> GetAll()
        {
            var planEtudeEtudes = _repository.GetAllPlanEtude();
            return Ok(planEtudeEtudes);
        }

        [Route("[action]/{codeClasse}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetAllPlanEtudeByClasse(string codeClasse)
        {
            var allPlanEtudeByClasse = _repository.GetAllPlanEtudeByClasse(codeClasse);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasse));
        }

        [Route("[action]/{codeClasse}/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetAllPlanEtudeByClasseAndAnnee(string codeClasse,
            string annee)
        {
            var allPlanEtudeByClasseAndAnnee = _repository.GetAllPlanEtudeByClasseAndAnnee(codeClasse, annee);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasseAndAnnee));
        }

        /* 
        // experimental 
        [Route("[action]/")]
        [HttpPost]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetAllPlanEtudeByCritere([FromBody] Criteria criteria)
        {
            var allPlanEtudeByClasse = _repository.GetAllPlanEtudeByEnseignant(criteria.listcriteria);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasse));
        }
        */

        [Route("[action]/{idEnseignant}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetAllPlanEtudeByEnseignant(string idEnseignant)
        {
            var allPlanEtudeByEnseignant = _repository.GetAllPlanEtudeByEnseignant(idEnseignant);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByEnseignant));
        }

        [Route("[action]/{year}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetAllPlanEtudeByAnnee(string year)
        {
            var allPlanEtudeByClasse = _repository.GetAllPlanEtudeByAnnee(year);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasse));
        }

        [Route("[action]/{module}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetAllPlanEtudeByModule(string module)
        {
            var allPlanEtudeByClasse = _repository.GetAllPlanEtudeByModule(module);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasse));
        }

        [Route("[action]/{classe}/{idEnseignant}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetAllPlanEtudeByClasseAndEnseignant(string classe,
            string idEnseignant)
        {
            var allPlanEtudeByClasseAndEnseignant =
                _repository.GetAllPlanEtudeByClasseAndEnseignant(classe, idEnseignant);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasseAndEnseignant));
        }

        [Route("[action]/{classe}/{idEnseignant}/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetAllPlanEtudeByClasseAndEnseignantAndAnnee(string classe,
            string idEnseignant, string annee)
        {
            var allPlanEtudeByClasseAndEnseignantAndAnnee =
                _repository.GetAllPlanEtudeByClasseAndEnseignantAndAnnee(classe, idEnseignant, annee);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasseAndEnseignantAndAnnee));
        }

        [Route("[action]/{classe}/{idEnseignant}/{annee}/{module}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetAllPlanEtudeByClasseAndEnseignantAndAnneeAndModule(
            string classe, string idEnseignant, string annee, string module)
        {
            var allPlanEtudeByClasseAndEnseignantAndAnneeAndModule =
                _repository.GetAllPlanEtudeByClasseAndEnseignantAndAnneeAndModule(classe, idEnseignant, annee, module);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasseAndEnseignantAndAnneeAndModule));
        }

        [HttpGet("{id}", Name = "GetPlanEtude")]
        public ActionResult<PlanEtudeReadDto> GetPlanEtude(string id)
        {
            var espPlanEtude = _repository.GetPlanEtudeById(id);

            if (espPlanEtude == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlanEtudeReadDto>(espPlanEtude));
        }

        [Route("[action]/{classe}/{module}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetAllPlanEtudesByClasseAndModule(string classe,
            string module)
        {
            var allPlanEtudeByClasseAndModule = _repository.GetAllPlanEtudesByClasseAndModule(classe, module);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasseAndModule));
        }

        [Route("[action]/{classe}/{module}/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetAllPlanEtudesByClasseAndModuleAndAnnee(string classe,
            string module, string annee)
        {
            var allPlanEtudeByClasseAndModuleAndAnnee =
                _repository.GetAllPlanEtudesByClasseAndModuleAndAnnee(classe, module, annee);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasseAndModuleAndAnnee));
        }

        [Route("[action]/{classe}/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetPlanEtudeByClasseAndAnne(string classe, string annee)
        {
            var allPlanEtudeByClasseAndAnnee = _repository.GetPlanEtudeByClasseAndAnne(classe, annee);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasseAndAnnee));
        }

        [Route("[action]/{idEnseignant}/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetPlanEtudeByEnseignantAndAnnee(string idEnseignant,
            string annee)
        {
            var allPlanEtudeByEnseignantAndAnnee = _repository.GetPlanEtudeByEnseignantAndAnnee(idEnseignant, annee);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByEnseignantAndAnnee));
        }

        [Route("[action]/{idEnseignant}/{module}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetPlanEtudeByEnseignantAndModule(string idEnseignant,
            string module)
        {
            var allPlanEtudeByEnseignantAndModule = _repository.GetPlanEtudeByEnseignantAndModule(idEnseignant, module);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByEnseignantAndModule));
        }

        [Route("[action]/{idEnseignant}/{module}/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetPlanEtudeByEnseignantAndModuleAndAnnee(
            string idEnseignant, string module, string annee)
        {
            var allPlanEtudeByEnseignantAndModuleAndAnnee =
                _repository.GetPlanEtudeByEnseignantAndModuleAndAnnee(idEnseignant, module, annee);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByEnseignantAndModuleAndAnnee));
        }

        [Route("[action]/{annee}/{module}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetPlanEtudeByAnneeAndModule(string annee, string module)
        {
            var allPlanEtudeByAnneeAndModule = _repository.GetPlanEtudeByAnneeAndModule(annee, module);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByAnneeAndModule));
        }

        [Route("[action]/{codeClasse}/{annee}/{semestre}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetAllPlanEtudeByClasseAndAnneeAndSemestre(string codeClasse,
            string annee, decimal semestre)
        {
            var allPlanEtudeByClasseAndAnneeAndSemestre =
                _repository.GetAllPlanEtudeByClasseAndAnneeAndSemestre(codeClasse, annee, semestre);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasseAndAnneeAndSemestre));
        }

        [HttpPost]
        public ActionResult<PlanEtudeReadDto> CreatePlanEtude(PlanEtudeCreateDto planEtudeCreateDto)
        {
            var planEtudeModel = _mapper.Map<EspModulePanierClasseSaiso>(planEtudeCreateDto);
            _repository.CreatePlanEtude(planEtudeModel);
            _repository.SaveChanges();
            var planEtudeReadDto = _mapper.Map<PlanEtudeReadDto>(planEtudeModel);
            return CreatedAtRoute(nameof(GetPlanEtude),
                new {Id = planEtudeReadDto.CodeModule}, planEtudeReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePlanEtude(string id, PlanEtudeUpdateDto planEtudeUpdateDto)
        {
            var planEtudeModelFromRepo = _repository.GetPlanEtudeById(id);
            if (planEtudeModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(planEtudeUpdateDto, planEtudeModelFromRepo);
            _repository.UpdatePlanEtude(planEtudeModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialPlanEtudeUpdate(string id, JsonPatchDocument<PlanEtudeUpdateDto> patchDoc)
        {
            var planEtudeModelFromRepo = _repository.GetPlanEtudeById(id);
            if (planEtudeModelFromRepo == null)
            {
                return NotFound();
            }

            var planEtudeToPatch = _mapper.Map<PlanEtudeUpdateDto>(planEtudeModelFromRepo);
            patchDoc.ApplyTo(planEtudeToPatch, ModelState);
            if (!TryValidateModel(planEtudeToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(planEtudeToPatch, planEtudeModelFromRepo);
            _repository.UpdatePlanEtude(planEtudeModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePlanEtude(string id)
        {
            var planEtudeModelFromRepo = _repository.GetPlanEtudeById(id);
            if (planEtudeModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeletePlanEtude(planEtudeModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }

    public class Criteria
    {
        public int id { get; set; }
        public string[] listcriteria { get; set; }
    }
}