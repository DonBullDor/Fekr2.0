using AutoMapper;
using Data.PlanEtude;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.Plan_etude;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpPost]
        public ActionResult<PlanEtudeReadDto> CreatePlanEtude(PlanEtudeCreateDto planEtudeCreateDto)
        {
            var planEtudeModel = _mapper.Map<EspModulePanierClasseSaiso>(planEtudeCreateDto);
            _repository.CreatePlanEtude(planEtudeModel);
            _repository.SaveChanges();
            var planEtudeReadDto = _mapper.Map<PlanEtudeReadDto>(planEtudeModel);
            return CreatedAtRoute(nameof(GetPlanEtude),
            new { Id = planEtudeReadDto.CodeModule }, planEtudeReadDto);
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
}
