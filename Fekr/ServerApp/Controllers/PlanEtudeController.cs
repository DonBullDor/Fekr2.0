using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Data.PlanEtude;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.Plan_etude;

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
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetAllPlanEtudeByClasse(string codeClasse)
        {
            var allPlanEtudeByClasse =
                _repository.GetAllPlanEtudeByClasse(codeClasse);
            return Ok(_mapper
                .Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasse));
        }

        [Route("[action]/{codeClasse}/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetAllPlanEtudeByClasseAndAnnee(string codeClasse, string annee)
        {
            var allPlanEtudeByClasseAndAnnee =
                _repository.GetAllPlanEtudeByClasseAndAnnee(codeClasse, annee);
            return Ok(_mapper
                .Map
                    <IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasseAndAnnee));
        }


        // experimental 
        [Route("[action]/")]
        [HttpPost]
        public ActionResult<IEnumerable<PlanEtudeReadDto>> GetAllPlanEtudeByCritere([FromBody] Criteria criteria)
        {
            var allPlanEtudeByClasse = _repository.GetAllPlanEtudeByCritere(criteria.listcriteria);
            return Ok(_mapper.Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasse));
        }

        [Route("[action]/{idEnseignant}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetAllPlanEtudeByEnseignant(string idEnseignant)
        {
            var allPlanEtudeByEnseignant =
                _repository.GetAllPlanEtudeByEnseignant(idEnseignant);
            return Ok(_mapper
                .Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByEnseignant));
        }

        [Route("[action]/{year}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetAllPlanEtudeByAnnee(string year)
        {
            var allPlanEtudeByClasse = _repository.GetAllPlanEtudeByAnnee(year);
            return Ok(_mapper
                .Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasse));
        }

        [Route("[action]/{module}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetAllPlanEtudeByModule(string module)
        {
            var allPlanEtudeByClasse =
                _repository.GetAllPlanEtudeByModule(module);
            return Ok(_mapper
                .Map<IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasse));
        }

        [Route("[action]/{classe}/{idEnseignant}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetAllPlanEtudeByClasseAndEnseignant(string classe, string idEnseignant)
        {
            var allPlanEtudeByClasseAndEnseignant =
                _repository
                    .GetAllPlanEtudeByClasseAndEnseignant(classe, idEnseignant);
            return Ok(_mapper
                .Map
                <IEnumerable<PlanEtudeReadDto>
                >(allPlanEtudeByClasseAndEnseignant));
        }

        [Route("[action]/{classe}/{idEnseignant}/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetAllPlanEtudeByClasseAndEnseignantAndAnnee(
                string classe,
                string idEnseignant,
                string annee
            )
        {
            var allPlanEtudeByClasseAndEnseignantAndAnnee =
                _repository
                    .GetAllPlanEtudeByClasseAndEnseignantAndAnnee(classe,
                        idEnseignant,
                        annee);
            return Ok(_mapper
                .Map
                <IEnumerable<PlanEtudeReadDto>
                >(allPlanEtudeByClasseAndEnseignantAndAnnee));
        }

        [Route("[action]/{classe}/{idEnseignant}/{annee}/{module}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetAllPlanEtudeByClasseAndEnseignantAndAnneeAndModule(
                string classe,
                string idEnseignant,
                string annee,
                string module
            )
        {
            var allPlanEtudeByClasseAndEnseignantAndAnneeAndModule =
                _repository
                    .GetAllPlanEtudeByClasseAndEnseignantAndAnneeAndModule(classe,
                        idEnseignant,
                        annee,
                        module);
            return Ok(_mapper
                .Map
                <IEnumerable<PlanEtudeReadDto>
                >(allPlanEtudeByClasseAndEnseignantAndAnneeAndModule));
        }

        [
            HttpGet(
                "{module}/{classe}/{annee}/{numSemestre:decimal}",
                Name = "GetPlanEtude")
        ]
        public ActionResult<PlanEtudeReadDto>
            GetPlanEtude(
                string module,
                string classe,
                string annee,
                decimal numSemestre
            )
        {
            var espPlanEtude =
                _repository
                    .GetPlanEtudeById(module, classe, annee, numSemestre);

            if (espPlanEtude == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlanEtudeReadDto>(espPlanEtude));
        }

        [Route("[action]/{classe}/{module}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetAllPlanEtudesByClasseAndModule(string classe, string module)
        {
            var allPlanEtudeByClasseAndModule =
                _repository.GetAllPlanEtudesByClasseAndModule(classe, module);
            return Ok(_mapper
                .Map
                    <IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasseAndModule));
        }

        [Route("[action]/{classe}/{module}/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetAllPlanEtudesByClasseAndModuleAndAnnee(
                string classe,
                string module,
                string annee
            )
        {
            var allPlanEtudeByClasseAndModuleAndAnnee =
                _repository
                    .GetAllPlanEtudesByClasseAndModuleAndAnnee(classe,
                        module,
                        annee);
            return Ok(_mapper
                .Map
                <IEnumerable<PlanEtudeReadDto>
                >(allPlanEtudeByClasseAndModuleAndAnnee));
        }

        [Route("[action]/{classe}/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetPlanEtudeByClasseAndAnne(string classe, string annee)
        {
            var allPlanEtudeByClasseAndAnnee =
                _repository.GetPlanEtudeByClasseAndAnne(classe, annee);
            return Ok(_mapper
                .Map
                    <IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByClasseAndAnnee));
        }

        [Route("[action]/{idEnseignant}/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetPlanEtudeByEnseignantAndAnnee(string idEnseignant, string annee)
        {
            var allPlanEtudeByEnseignantAndAnnee =
                _repository
                    .GetPlanEtudeByEnseignantAndAnnee(idEnseignant, annee);
            return Ok(_mapper
                .Map
                <IEnumerable<PlanEtudeReadDto>
                >(allPlanEtudeByEnseignantAndAnnee));
        }

        [Route("[action]/{idEnseignant}/{module}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetPlanEtudeByEnseignantAndModule(string idEnseignant, string module)
        {
            var allPlanEtudeByEnseignantAndModule =
                _repository
                    .GetPlanEtudeByEnseignantAndModule(idEnseignant, module);
            return Ok(_mapper
                .Map
                <IEnumerable<PlanEtudeReadDto>
                >(allPlanEtudeByEnseignantAndModule));
        }

        [Route("[action]/{idEnseignant}/{module}/{annee}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetPlanEtudeByEnseignantAndModuleAndAnnee(
                string idEnseignant,
                string module,
                string annee
            )
        {
            var allPlanEtudeByEnseignantAndModuleAndAnnee =
                _repository
                    .GetPlanEtudeByEnseignantAndModuleAndAnnee(idEnseignant,
                        module,
                        annee);
            return Ok(_mapper
                .Map
                <IEnumerable<PlanEtudeReadDto>
                >(allPlanEtudeByEnseignantAndModuleAndAnnee));
        }

        [Route("[action]/{annee}/{module}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetPlanEtudeByAnneeAndModule(string annee, string module)
        {
            var allPlanEtudeByAnneeAndModule =
                _repository.GetPlanEtudeByAnneeAndModule(annee, module);
            return Ok(_mapper
                .Map
                    <IEnumerable<PlanEtudeReadDto>>(allPlanEtudeByAnneeAndModule));
        }

        [Route("[action]/{codeClasse}/{annee}/{semestre}")]
        [HttpGet]
        public ActionResult<IEnumerable<PlanEtudeReadDto>>
            GetAllPlanEtudeByClasseAndAnneeAndSemestre(
                string codeClasse,
                string annee,
                decimal semestre
            )
        {
            var allPlanEtudeByClasseAndAnneeAndSemestre =
                _repository
                    .GetAllPlanEtudeByClasseAndAnneeAndSemestre(codeClasse,
                        annee,
                        semestre);
            return Ok(_mapper
                .Map
                <IEnumerable<PlanEtudeReadDto>
                >(allPlanEtudeByClasseAndAnneeAndSemestre));
        }

        [HttpPost]
        public ActionResult<PlanEtudeReadDto>
            CreatePlanEtude(PlanEtudeCreateDto planEtudeCreateDto)
        {
            var planEtudeModel =
                _mapper.Map<EspModulePanierClasseSaiso>(planEtudeCreateDto);
            _repository.CreatePlanEtude(planEtudeModel);
            _repository.SaveChanges();
            var planEtudeReadDto =
                _mapper.Map<PlanEtudeReadDto>(planEtudeModel);
            return CreatedAtRoute(nameof(GetPlanEtude),
                new {Id = planEtudeReadDto.CodeModule},
                planEtudeReadDto);
        }

        [HttpPut("{module}/{classe}/{annee}/{numSemestre:decimal}")]
        public ActionResult
            UpdatePlanEtude(
                string module,
                string classe,
                string annee,
                decimal numSemestre,
                PlanEtudeUpdateDto planEtudeUpdateDto
            )
        {
            var planEtudeModelFromRepo =
                _repository
                    .GetPlanEtudeById(module, classe, annee, numSemestre);
            if (planEtudeModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(planEtudeUpdateDto, planEtudeModelFromRepo);
            _repository.UpdatePlanEtude(planEtudeModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{module}/{classe}/{annee}/{numSemestre:decimal}")]
        public ActionResult
            PartialPlanEtudeUpdate(
                string module,
                string classe,
                string annee,
                decimal numSemestre,
                JsonPatchDocument<PlanEtudeUpdateDto> patchDoc
            )
        {
            var planEtudeModelFromRepo =
                _repository
                    .GetPlanEtudeById(module, classe, annee, numSemestre);
            if (planEtudeModelFromRepo == null)
            {
                return NotFound();
            }

            var planEtudeToPatch =
                _mapper.Map<PlanEtudeUpdateDto>(planEtudeModelFromRepo);
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

        [HttpDelete("{module}/{classe}/{annee}/{numSemestre:decimal}")]
        public ActionResult
            DeletePlanEtude(
                string module,
                string classe,
                string annee,
                decimal numSemestre
            )
        {
            var planEtudeModelFromRepo =
                _repository
                    .GetPlanEtudeById(module, classe, annee, numSemestre);
            if (planEtudeModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeletePlanEtude(planEtudeModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [Route("[action]/{annee}")]
        [HttpGet]
        public ActionResult GetNumberOfClasses(string annee)
        {
            var total = _repository.GetNumberOfClasses(annee);
            return Ok(total);
        }

        [Route("[action]/{classe}/{annee}")]
        [HttpGet]
        public ActionResult GetNumberOfModulesOfClassByYear(string classe, string annee)
        {
            var total = _repository.GetNumberOfModulesOfClassByYear(classe, annee);
            return Ok(total);
        }

        [Route("[action]/{classe}/{annee}")]
        [HttpGet]
        public ActionResult GetModulesByClasseAndYear(string classe, string annee)
        {
            var total = _repository.GetModulesByClasseAndYear(classe, annee);
            return Ok(total);
        }

        [Route("[action]/{annee}")]
        [HttpGet]
        public ActionResult GetListOfAllClassesByYear(string annee)
        {
            var total = _repository.GetListOfAllClassesByYear(annee);
            return Ok(total);
        }
        
        [Route("[action]/{annee}/{module}")]
        [HttpGet]
        public ActionResult GetListOfEnseignantByModule(string annee, string module)
        {
            var total = _repository.GetListOfEnseignantByModule(annee, module);
            return Ok(total);
        }
        
        [Route("[action]/{annee}/{module}/{classe}")]
        [HttpGet]
        public ActionResult GetNbHeurModuleParClasse(string annee, string module, string classe)
        {
            var total = _repository.GetNbHeureParModuleParClasse(annee, module, classe);
            return Ok(total);
        }

        private class ModuleEnseignant
        {
            public string module {
                get;
                set;
            }

            public string enseignant {
                get;
                set;
            }

            public decimal nbHeure
            {
                get;
                set;
            }
            
            public decimal NumSemestre
            {
                get;
                set;
            }
        }
        
        [Route("[action]")]
        [HttpGet]
        public ActionResult GeneratePlanEtude()
        {
            var numberOfClasses = _repository.GetNumberOfClasses("2020");
            var numberOfModulesOf7Eme = _repository.GetNumberOfModulesOfClassByYear("7B2", "2020");
            var numberOfModulesOf8Eme = _repository.GetNumberOfModulesOfClassByYear("8B1", "2020");
            var numberOfModulesOf9Eme = _repository.GetNumberOfModulesOfClassByYear("9B1", "2020");
            var numberOfModulesOf1S = _repository.GetNumberOfModulesOfClassByYear("1S1", "2020");
            var numberOfModulesOf2Eco = _repository.GetNumberOfModulesOfClassByYear("2ECO1", "2020");
            var numberOfModulesOf2Info = _repository.GetNumberOfModulesOfClassByYear("2INFO1", "2020");
            var numberOfModulesOf2Science = _repository.GetNumberOfModulesOfClassByYear("2SC1", "2020");
            var numberOfModulesOf3Eco = _repository.GetNumberOfModulesOfClassByYear("3ECO1", "2020");
            var numberOfModulesOf3Math = _repository.GetNumberOfModulesOfClassByYear("3M1", "2020");
            var numberOfModulesOf3Science = _repository.GetNumberOfModulesOfClassByYear("3SE1", "2020");
            var numberOfModulesOf3Technique = _repository.GetNumberOfModulesOfClassByYear("3TECH1", "2020");
            var numberOfModulesOf4Math = _repository.GetNumberOfModulesOfClassByYear("4M1", "2020");
            var numberOfModulesOf4Science = _repository.GetNumberOfModulesOfClassByYear("4SE1", "2020");

            var numberOfModules = new[]
            {
                numberOfModulesOf7Eme,
                numberOfModulesOf8Eme,
                numberOfModulesOf9Eme,
                numberOfModulesOf1S,
                numberOfModulesOf2Eco,
                numberOfModulesOf2Info,
                numberOfModulesOf2Science,
                numberOfModulesOf3Eco,
                numberOfModulesOf3Math,
                numberOfModulesOf3Science,
                numberOfModulesOf3Technique,
                numberOfModulesOf4Math,
                numberOfModulesOf4Science
            };

            var modulesOf7Eme = _repository.GetModulesByClasseAndYear("7B2", "2020");
            var modulesOf8Eme = _repository.GetModulesByClasseAndYear("8B1", "2020");
            var modulesOf9Eme = _repository.GetModulesByClasseAndYear("9B1", "2020");
            var modulesOf1S = _repository.GetModulesByClasseAndYear("1S1", "2020");
            var modulesOf2Eco = _repository.GetModulesByClasseAndYear("2ECO1", "2020");
            var modulesOf2Info = _repository.GetModulesByClasseAndYear("2INFO1", "2020");
            var modulesOf2Science = _repository.GetModulesByClasseAndYear("2SC1", "2020");
            var modulesOf3Eco = _repository.GetModulesByClasseAndYear("3ECO1", "2020");
            var modulesOf3Math = _repository.GetModulesByClasseAndYear("3M1", "2020");
            var modulesOf3Science = _repository.GetModulesByClasseAndYear("3SE1", "2020");
            var modulesOf3Technique = _repository.GetModulesByClasseAndYear("3TECH1", "2020");
            var modulesOf4Math = _repository.GetModulesByClasseAndYear("4M1", "2020");
            var modulesOf4Science = _repository.GetModulesByClasseAndYear("4SE1", "2020");

            var modulesByClasse = new
            {
                modulesOf7Eme,
                modulesOf8Eme,
                modulesOf9Eme,
                modulesOf1S,
                modulesOf2Eco,
                modulesOf2Info,
                modulesOf2Science,
                modulesOf3Eco,
                modulesOf3Math,
                modulesOf3Science,
                modulesOf3Technique,
                modulesOf4Math,
                modulesOf4Science
            };

            var listOfAllClasses = _repository.GetListOfAllClassesByYear("2020");
            var refinedListOfClasses = listOfAllClasses.Distinct();
            var listOfClasses = refinedListOfClasses as string[] ?? refinedListOfClasses.ToArray();

            var moduleParClass = new Dictionary<string, List<ModuleEnseignant>>();

            for (var i = 0; i < numberOfClasses; i++)
            {
                
                //moduleParClass.Add(listOfClasses[i] ?? string.Empty, modulesOf7Eme.ToList());
                if (listOfClasses[i].StartsWith('7'))
                {
                    var moduleEnseignants = new List<ModuleEnseignant>();
                    foreach (var module in modulesOf7Eme)
                    {
                        var listEnseignantParModule = _repository.GetListOfEnseignantByModule("2020", module);
                        var nbHeureParModule =
                            _repository.GetNbHeureParModuleParClasse("2020", module, listOfClasses[i]);
                        
                        var enseignant = listEnseignantParModule[new Random().Next(0, listEnseignantParModule.Length-1)];
                        moduleEnseignants.Add(new ModuleEnseignant()
                        {
                            enseignant = enseignant,
                            module = module,
                            nbHeure = nbHeureParModule
                        });
                    }
                    moduleParClass.Add(listOfClasses[i] ?? string.Empty, moduleEnseignants);
                }
                
                else if (listOfClasses[i].StartsWith('8'))
                {
                    var moduleEnseignants = new List<ModuleEnseignant>();
                    foreach (var module in modulesOf8Eme)
                    {
                        var listEnseignantParModule = _repository.GetListOfEnseignantByModule("2020", module);
                        var nbHeureParModule =
                            _repository.GetNbHeureParModuleParClasse("2020", module, listOfClasses[i]);
                        string enseignant = listEnseignantParModule[new Random().Next(0, listEnseignantParModule.Length-1)];
                        moduleEnseignants.Add(new ModuleEnseignant()
                        {
                            enseignant = enseignant,
                            module = module,
                            nbHeure = nbHeureParModule
                        });
                    }
                    moduleParClass.Add(listOfClasses[i] ?? string.Empty, moduleEnseignants);
                }
                else if (listOfClasses[i].StartsWith('9'))
                {
                    var moduleEnseignants = new List<ModuleEnseignant>();
                    foreach (var module in modulesOf9Eme)
                    {
                        var listEnseignantParModule = _repository.GetListOfEnseignantByModule("2020", module);
                        var nbHeureParModule =
                            _repository.GetNbHeureParModuleParClasse("2020", module, listOfClasses[i]);
                        string enseignant = listEnseignantParModule[new Random().Next(0, listEnseignantParModule.Length-1)];
                        moduleEnseignants.Add(new ModuleEnseignant()
                        {
                            enseignant = enseignant,
                            module = module,
                            nbHeure = nbHeureParModule
                        });
                    }
                    moduleParClass.Add(listOfClasses[i] ?? string.Empty, moduleEnseignants);                }
                else if (listOfClasses[i].StartsWith('1'))
                {
                    var moduleEnseignants = new List<ModuleEnseignant>();
                    foreach (var module in modulesOf1S)
                    {
                        var listEnseignantParModule = _repository.GetListOfEnseignantByModule("2020", module);
                        var nbHeureParModule =
                            _repository.GetNbHeureParModuleParClasse("2020", module, listOfClasses[i]);
                        string enseignant = listEnseignantParModule[new Random().Next(0, listEnseignantParModule.Length-1)];
                        moduleEnseignants.Add(new ModuleEnseignant()
                        {
                            enseignant = enseignant,
                            module = module,
                            nbHeure = nbHeureParModule
                        });
                    }
                    moduleParClass.Add(listOfClasses[i] ?? string.Empty, moduleEnseignants);                }
                
                else if (listOfClasses[i].IndexOf("2 S") == 0)
                {
                    var moduleEnseignants = new List<ModuleEnseignant>();
                    foreach (var module in modulesOf1S)
                    {
                        var listEnseignantParModule = _repository.GetListOfEnseignantByModule("2020", module);
                        var nbHeureParModule =
                            _repository.GetNbHeureParModuleParClasse("2020", module, listOfClasses[i]);
                        string enseignant = listEnseignantParModule[new Random().Next(0, listEnseignantParModule.Length-1)];
                        moduleEnseignants.Add(new ModuleEnseignant()
                        {
                            enseignant = enseignant,
                            module = module,
                            nbHeure = nbHeureParModule
                        });
                    }
                    moduleParClass.Add(listOfClasses[i] ?? string.Empty, moduleEnseignants);
                }
                else if (listOfClasses[i].IndexOf("3 S") == 0)
                {
                    var moduleEnseignants = new List<ModuleEnseignant>();
                    foreach (var module in modulesOf3Science)
                    {
                        var listEnseignantParModule = _repository.GetListOfEnseignantByModule("2020", module);
                        var nbHeureParModule =
                            _repository.GetNbHeureParModuleParClasse("2020", module, listOfClasses[i]);
                        string enseignant = listEnseignantParModule[new Random().Next(0, listEnseignantParModule.Length-1)];
                        moduleEnseignants.Add(new ModuleEnseignant()
                        {
                            enseignant = enseignant,
                            module = module,
                            nbHeure = nbHeureParModule
                        });
                    }
                    moduleParClass.Add(listOfClasses[i] ?? string.Empty, moduleEnseignants);
                }
                else if (listOfClasses[i].IndexOf("4 S") == 0)
                {
                    var moduleEnseignants = new List<ModuleEnseignant>();
                    foreach (var module in modulesOf4Science)
                    {
                        var listEnseignantParModule = _repository.GetListOfEnseignantByModule("2020", module);
                        string enseignant = listEnseignantParModule[new Random().Next(0, listEnseignantParModule.Length-1)];
                        moduleEnseignants.Add(new ModuleEnseignant()
                        {
                            enseignant = enseignant,
                            module = module,
                            nbHeure = 0
                        });
                    }
                    moduleParClass.Add(listOfClasses[i] ?? string.Empty, moduleEnseignants);
                }
                else if (listOfClasses[i].IndexOf("3 M") == 0)
                {
                    var moduleEnseignants = new List<ModuleEnseignant>();
                    foreach (var module in modulesOf3Math)
                    {
                        var listEnseignantParModule = _repository.GetListOfEnseignantByModule("2020", module);
                        var nbHeureParModule =
                            _repository.GetNbHeureParModuleParClasse("2020", module, listOfClasses[i]);
                        string enseignant = listEnseignantParModule[new Random().Next(0, listEnseignantParModule.Length-1)];
                        moduleEnseignants.Add(new ModuleEnseignant()
                        {
                            enseignant = enseignant,
                            module = module,
                            nbHeure = nbHeureParModule
                        });
                    }
                    moduleParClass.Add(listOfClasses[i] ?? string.Empty, moduleEnseignants);
                }
                else if (listOfClasses[i].IndexOf("4 M") == 0)
                {
                    var moduleEnseignants = new List<ModuleEnseignant>();
                    foreach (var module in modulesOf4Math)
                    {
                        var listEnseignantParModule = _repository.GetListOfEnseignantByModule("2020", module);
                        var nbHeureParModule =
                            _repository.GetNbHeureParModuleParClasse("2020", module, listOfClasses[i]);
                        string enseignant = listEnseignantParModule[new Random().Next(0, listEnseignantParModule.Length-1)];
                        moduleEnseignants.Add(new ModuleEnseignant()
                        {
                            enseignant = enseignant,
                            module = module,
                            nbHeure = nbHeureParModule
                        });
                    }
                    moduleParClass.Add(listOfClasses[i] ?? string.Empty, moduleEnseignants);
                }
                else if (listOfClasses[i].IndexOf("2 E") == 0)
                {
                    var moduleEnseignants = new List<ModuleEnseignant>();
                    foreach (var module in modulesOf2Eco)
                    {
                        var listEnseignantParModule = _repository.GetListOfEnseignantByModule("2020", module);
                        var nbHeureParModule =
                            _repository.GetNbHeureParModuleParClasse("2020", module, listOfClasses[i]);
                        string enseignant = listEnseignantParModule[new Random().Next(0, listEnseignantParModule.Length-1)];
                        moduleEnseignants.Add(new ModuleEnseignant()
                        {
                            enseignant = enseignant,
                            module = module,
                            nbHeure = nbHeureParModule
                        });
                    }
                    moduleParClass.Add(listOfClasses[i] ?? string.Empty, moduleEnseignants);
                }
                else if (listOfClasses[i].IndexOf("3 E") == 0)
                {
                    var moduleEnseignants = new List<ModuleEnseignant>();
                    foreach (var module in modulesOf3Eco)
                    {
                        var listEnseignantParModule = _repository.GetListOfEnseignantByModule("2020", module);
                        var nbHeureParModule =
                            _repository.GetNbHeureParModuleParClasse("2020", module, listOfClasses[i]);
                        string enseignant = listEnseignantParModule[new Random().Next(0, listEnseignantParModule.Length-1)];
                        moduleEnseignants.Add(new ModuleEnseignant()
                        {
                            enseignant = enseignant,
                            module = module,
                            nbHeure = nbHeureParModule
                        });
                    }
                    moduleParClass.Add(listOfClasses[i] ?? string.Empty, moduleEnseignants);
                }
                else if (listOfClasses[i].IndexOf("2 I") == 0)
                {
                    var moduleEnseignants = new List<ModuleEnseignant>();
                    foreach (var module in modulesOf2Info)
                    {
                        var listEnseignantParModule = _repository.GetListOfEnseignantByModule("2020", module);
                        var nbHeureParModule =
                            _repository.GetNbHeureParModuleParClasse("2020", module, listOfClasses[i]);
                        string enseignant = listEnseignantParModule[new Random().Next(0, listEnseignantParModule.Length-1)];
                        moduleEnseignants.Add(new ModuleEnseignant()
                        {
                            enseignant = enseignant,
                            module = module,
                            nbHeure = nbHeureParModule
                        });
                    }
                    moduleParClass.Add(listOfClasses[i] ?? string.Empty, moduleEnseignants);
                }
                else if (listOfClasses[i].IndexOf("3 T") == 0)
                {
                    var moduleEnseignants = new List<ModuleEnseignant>();
                    foreach (var module in modulesOf3Technique)
                    {
                        var listEnseignantParModule = _repository.GetListOfEnseignantByModule("2020", module);
                        var nbHeureParModule =
                            _repository.GetNbHeureParModuleParClasse("2020", module, listOfClasses[i]);
                        string enseignant = listEnseignantParModule[new Random().Next(0, listEnseignantParModule.Length-1)];
                        moduleEnseignants.Add(new ModuleEnseignant()
                        {
                            enseignant = enseignant,
                            module = module,
                            nbHeure = nbHeureParModule
                        });
                    }
                    moduleParClass.Add(listOfClasses[i] ?? string.Empty, moduleEnseignants);
                }
                else if (listOfClasses[i].IndexOf("4 M") == 0)
                {
                    var moduleEnseignants = new List<ModuleEnseignant>();
                    foreach (var module in modulesOf4Math)
                    {
                        var listEnseignantParModule = _repository.GetListOfEnseignantByModule("2020", module);
                        var nbHeureParModule =
                            _repository.GetNbHeureParModuleParClasse("2020", module, listOfClasses[i]);
                        string enseignant = listEnseignantParModule[new Random().Next(0, listEnseignantParModule.Length-1)];
                        moduleEnseignants.Add(new ModuleEnseignant()
                        {
                            enseignant = enseignant,
                            module = module,
                            nbHeure = nbHeureParModule
                        });
                    }
                    moduleParClass.Add(listOfClasses[i] ?? string.Empty, moduleEnseignants);
                }
                

            }

            //refinedListOfClasses || moduleParClass || listOfClasses
            return Ok(moduleParClass);
        }
    }

    public class Criteria
    {
        public int id { get; set; }

        public string[] listcriteria { get; set; }
    }
}