using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using Data.EmploiDuTemp;
using Data.PlanEtude;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerApp.Models;
using Service.Repository.EmploiDuTemp;
using Service.Repository.Modules;
using Service.Repository.Plan_etude;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmploiDuTempController : Controller
    {
        private readonly IEmploiDuTempRepo _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmploiDuTempController> _logger; 
        private readonly IModuleApiRepo _repoModule;
        private readonly IPlanEtudeApiRepo _repoPlan;
        public EmploiDuTempController(
            IEmploiDuTempRepo repository,
            IMapper mapper,
            IModuleApiRepo moduleRepo,
            IPlanEtudeApiRepo planRepo,
            ILogger<EmploiDuTempController> log)
        {
            _repository = repository;
            _mapper = mapper;
            _repoModule = moduleRepo;
            _repoPlan = planRepo;
            _logger = log;
        }

        public EmploiDuTempController(IEmploiDuTempRepo mockRepoObject, IMapper mapper)
        {
            _repository = mockRepoObject;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EspEmploi>> GetAll()
        {
            var emploiDuTemp = _repository.GetAllEmploiDuTemp();
            return Ok(emploiDuTemp);
        }
        
        [Route("[action]/{codeModule}", Name = "GetEmploiDuTempByCodeModule")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>> GetEmploiDuTempByCodeModule(string codeModule)
        {
            var emploiDuTempByCodeModule = _repository.GetEmploiDuTempByCodeModule(codeModule);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(emploiDuTempByCodeModule));
        }
        
        [Route("[action]/{codeModule}")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>> GetAllEmploiDuTempByCodeModule(string codeModule)
        {
            var allEmploiDuTempByCodeModule = _repository.GetAllEmploiDuTempByCodeModule(codeModule);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByCodeModule));
        }
        
        [Route("[action]/{codeModule}/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>> GetAllEmploiDuTempByCodeModuleAndAnnee(string codeModule, string annee)
        {
            var allEmploiDuTempByCodeModuleAndAnnee = _repository.GetAllEmploiDuTempByCodeModuleAndAnnee(codeModule, annee);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByCodeModuleAndAnnee));
        }
        
        [Route("[action]/{codeModule}/{annee}/{classe}")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>> GetAllEmploiDuTempByCodeModuleAndAnnee(string codeModule, string annee, string classe)
        {
            var allEmploiDuTempByCodeModuleAndAnneeAndClasse = _repository.GetAllEmploiDuTempByCodeModuleAndAnneeAndClasse(codeModule, annee, classe);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByCodeModuleAndAnneeAndClasse));
        }
        
        [Route("[action]/{codeModule}/{classe}")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>> GetAllEmploiDuTempByCodeModuleAndAnneeAndClasse(string codeModule, string classe)
        {
            var allEmploiDuTempByCodeModuleAndClasse = _repository.GetAllEmploiDuTempByCodeModuleAndClasse(codeModule, classe);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByCodeModuleAndClasse));
        }
        
        [Route("[action]/{codeModule}/numSeance")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>> GetAllEmploiDuTempByCodeModuleAndAnneeAndNumSeance(string codeModule, decimal numSeance)
        {
            var allEmploiDuTempByCodeModuleAndNumSeance = _repository.GetAllEmploiDuTempByCodeModuleAndNumSeance(codeModule, numSeance);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByCodeModuleAndNumSeance));
        }
        
        [Route("[action]/{codeModule}/{annee}/numSeance")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>> GetAllEmploiDuTempByCodeModuleAndAnneeAndNumSeance(string codeModule, string annee, decimal numSeance)
        {
            var allEmploiDuTempByCodeModuleAndAnneeAndNumSeance = _repository.GetAllEmploiDuTempByCodeModuleAndAnneeAndNumSeance(codeModule, annee, numSeance);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByCodeModuleAndAnneeAndNumSeance));
        }
        
        [Route("[action]/{codeModule}/{annee}/{jour}")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>> GetAllEmploiDuTempByCodeModuleAndAnneeAndJour(string codeModule, string annee, string jour)
        {
            var allEmploiDuTempByCodeModuleAndAnneeAndJour = _repository.GetAllEmploiDuTempByCodeModuleAndAnneeAndJour(codeModule, annee, jour);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByCodeModuleAndAnneeAndJour));
        }
        
        [Route("[action]/{codeModule}/{jour}")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>> GetAllEmploiDuTempByCodeModuleAndJour(string codeModule, string jour)
        {
            var allEmploiDuTempByCodeModuleAndJour = _repository.GetAllEmploiDuTempByCodeModuleAndJour(codeModule, jour);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByCodeModuleAndJour));
        }
        
        [Route("[action]/{codeModule}/{typeSeance}")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>> GetAllEmploiDuTempByCodeModuleAndTypeSeance(string codeModule, string typeSeance)
        {
            var allEmploiDuTempByCodeModuleAndTypeSeance = _repository.GetAllEmploiDuTempByCodeModuleAndTypeSeance(codeModule, typeSeance);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByCodeModuleAndTypeSeance));
        }
        
        [Route("[action]/{codeClasse}")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>> GetAllEmploiDuTempByClasse(string codeClasse)
        {
            var allEmploiDuTempByClasse = _repository.GetAllEmploiDuTempByClasse(codeClasse);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByClasse));
        }
        
        [Route("[action]/{codeClasse}/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>>  GetAllEmploiDuTempByClasseAndAnnee(string codeClasse, string annee)
        {
            var allEmploiDuTempByClasseAndAnnee = _repository.GetAllEmploiDuTempByClasseAndAnnee(codeClasse, annee);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByClasseAndAnnee));
        }
        
        [Route("[action]/{codeClasse}/semestre")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>>  GetAllEmploiDuTempByClasseAndSemestre(string codeClasse, decimal semestre)
        {
            var allEmploiDuTempByClasseAndSemestre = _repository.GetAllEmploiDuTempByClasseAndSemestre(codeClasse, semestre);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByClasseAndSemestre));
        }
        
        [Route("[action]/{codeClasse}/numSeance")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>>  GetAllEmploiDuTempByClasseAndNumSeance(string codeClasse, decimal numSeance)
        {
            var allEmploiDuTempByClasseAndNumSeance = _repository.GetAllEmploiDuTempByClasseAndNumSeance(codeClasse, numSeance);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByClasseAndNumSeance));
        }
        
        [Route("[action]/{codeClasse}/{jour}")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>>  GetAllEmploiDuTempByClasseAndJour(string codeClasse, string jour)
        {
            var allEmploiDuTempByClasseAndJour = _repository.GetAllEmploiDuTempByClasseAndJour(codeClasse, jour);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByClasseAndJour));
        }
        
        [Route("[action]/{codeClasse}/{typeSeance}")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>>  GetAllEmploiDuTempByClasseAndTypeSeance(string codeClasse, string typeSeance)
        {
            var allEmploiDuTempByClasseAndTypeSeance = _repository.GetAllEmploiDuTempByClasseAndTypeSeance(codeClasse, typeSeance);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByClasseAndTypeSeance));
        }
        
        [Route("[action]/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>>  GetAllEmploiDuTempByClasseAndTypeSeance(string annee)
        {
            var allEmploiDuTempByAnnee = _repository.GetAllEmploiDuTempByAnnee(annee);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByAnnee));
        }
        
        [Route("[action]/{annee}/numSeance")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>>  GetAllEmploiDuTempByAnneeAndNumSeance(string annee, decimal numSeance)
        {
            var allEmploiDuTempByAnneeAndNumSeance = _repository.GetAllEmploiDuTempByAnneeAndNumSeance(annee, numSeance);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByAnneeAndNumSeance));
        }
        
        [Route("[action]/{annee}/semestre")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>>  GetAllEmploiDuTempByAnneeAndSemestre(string annee, decimal semestre)
        {
            var allEmploiDuTempByAnneeAndSemestre = _repository.GetAllEmploiDuTempByAnneeAndSemestre(annee, semestre);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempByAnneeAndSemestre));
        }
        
        [Route("[action]/semestre")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>>  GetAllEmploiDuTempBySemestre(decimal semestre)
        {
            var allEmploiDuTempBySemestre = _repository.GetAllEmploiDuTempBySemestre(semestre);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempBySemestre));
        }
        
        [Route("[action]/semestre/{jour}")]
        [HttpGet]
        public ActionResult<IEnumerable<EmploiDuTempReadDto>>  GetAllEmploiDuTempBySemestre(decimal semestre, string jour)
        {
            var allEmploiDuTempBySemestreAndJour = _repository.GetAllEmploiDuTempBySemestreAndJour(semestre, jour);
            return Ok(_mapper.Map<IEnumerable<EmploiDuTempReadDto>>(allEmploiDuTempBySemestreAndJour));
        }
        
        [HttpPost]
        public ActionResult<EmploiDuTempReadDto> CreateEmploiDuTemp(EmploiDuTempCreateDto emploiDuTempCreateDto)
        {
            var emploiDuTempModel = _mapper.Map<EspEmploi>(emploiDuTempCreateDto);
            _repository.CreateEmploiDuTemp(emploiDuTempModel);
            _repository.SaveChanges();
            var planEtudeReadDto = _mapper.Map<PlanEtudeReadDto>(emploiDuTempModel);
            return CreatedAtRoute(nameof(GetEmploiDuTempByCodeModule),
                new { Id = planEtudeReadDto.CodeModule }, planEtudeReadDto);
        }
        
        [HttpPut("{id}")]
        public ActionResult UpdateEmploiDuTemp(string id, EmploiDuTempUpdateDto emploiDuTemp)
        {
            var emploiDuTempModelFromRepo = _repository.GetEmploiDuTempByCodeModule(id);
            if (emploiDuTempModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(emploiDuTemp, emploiDuTempModelFromRepo);
            _repository.UpdateEmploiDuTemp(emploiDuTempModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        
        [HttpPatch("{id}")]
        public ActionResult PartialEmploiDuTempUpdate(string id, JsonPatchDocument<EmploiDuTempUpdateDto> patchDoc)
        {
            var emploiDuTempModelFromRepo = _repository.GetEmploiDuTempByCodeModule(id);
            if (emploiDuTempModelFromRepo == null)
            {
                return NotFound();
            }
            var emploiDuTempToPatch = _mapper.Map<EmploiDuTempUpdateDto>(emploiDuTempModelFromRepo);
            patchDoc.ApplyTo(emploiDuTempToPatch, ModelState);
            if (!TryValidateModel(emploiDuTempToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(emploiDuTempToPatch, emploiDuTempModelFromRepo);
            _repository.UpdateEmploiDuTemp(emploiDuTempModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteEmploiDuTemp(string id)
        {
            var emploiDuTempModelFromRepo = _repository.GetEmploiDuTempByCodeModule(id);
            if (emploiDuTempModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteEmploiDuTemp(emploiDuTempModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        //[Route("[action]/{codeModule}", Name = "GetEmploiDuTempByCodeModule")]
        
        /**
         * chargeHoraire: Dict<string, decimal> : string = code module, decimal = nb heure par semaine
         * jours: les jours de la semaine jusqu'à le jour j
         * emploi: l'emploi avec les modules inserée, vide au debut
         * moduleToInsert: le module qu'on va inserer
         * returns: nombre d'heure restant pour le module qu'on va inserer
         */
        private static decimal HoursLeft(
            Dictionary<string, decimal> chargeHoraire, 
            String[] jours, 
            Dictionary<string, List<string>> emploi,
            string moduleToInsert)
        {
            if (!chargeHoraire.ContainsKey(moduleToInsert))
            {
                return 0;
            }
            var sumHours = 0;
            var i = 0;
            while (i<jours.Length)
            {
                sumHours += emploi[jours[i]].FindAll(module => module == moduleToInsert).Count;
                i++;
            }

            return chargeHoraire[moduleToInsert] - sumHours;
        }
        
        // TODO liste des jours feriers + d'autres contraintes (e.g.: math doit etre separe par un jours)
        [HttpGet]
        [Route("[action]/{codeClasse}/{annee}/{semestre:decimal}")]
        public ActionResult GenerateEmploiDuTemp(string codeClasse, string annee, decimal semestre)
        {
            // Charge Horaire Init : string = code Module, decimal = nombre d'heure par semaine
            var chargeHoraire = new Dictionary<string, decimal>();
            var plan = 
                _repoPlan.GetAllPlanEtudeByClasseAndAnneeAndSemestre(codeClasse, annee, semestre);

            var espModulePanierClasseSaisos = plan.ToList();
            if (!espModulePanierClasseSaisos.Any()) return BadRequest("Check your values");
            
            foreach (var item in espModulePanierClasseSaisos)
            {
                if (item.NbHeures != null)
                {
                    if (!chargeHoraire.ContainsKey(item.CodeModule))
                    {
                        chargeHoraire.Add(item.CodeModule, decimal.Ceiling((decimal) item.NbHeures / 12));
                    }
                }
                else
                {
                    if (!chargeHoraire.ContainsKey(item.CodeModule))
                    {
                        chargeHoraire.Add(item.CodeModule, 0);
                    }
                }
            }

            var emploi = new Dictionary<string, List<string>>
            {
                {"lundi", new List<string>(9)},
                {"mardi", new List<string>(9)},
                {"mercredi", new List<string>(9)},
                {"jeudi", new List<string>(9)},
                {"vendredi", new List<string>(9)},
                {"samedi", new List<string>(5)}
            };

            var rng = new Random();
            string[] jours = {"lundi", "mardi", "mercredi", "jeudi", "vendredi", "samedi"};
            List<string> previousDates = new List<string>();
            foreach(String jour in jours)
            {
                previousDates.Add(jour);
                List<string> horaires = emploi[jour];
                for (var i = 0; i < 7; i++)
                {
                    bool inserted = false;
                    while (!inserted)
                    {
                        string moduleToInsert = chargeHoraire.ElementAt(rng.Next(0, chargeHoraire.Count)).Key;
                        var hoursLeft = HoursLeft(chargeHoraire, previousDates.ToArray(), emploi, moduleToInsert);

                        if (hoursLeft <= 0)
                        {
                            chargeHoraire.Remove(moduleToInsert);
                        }
                        else if (hoursLeft % 2 == 0 && horaires.Count <= emploi[jour].Capacity-2)
                        {
                            horaires.Add(moduleToInsert);
                            horaires.Add(moduleToInsert);
                        }
                        else
                        {
                            horaires.Add(moduleToInsert);
                        }

                        inserted = true;
                    }
                }
            }
            return Ok(emploi);

        }
    }
}