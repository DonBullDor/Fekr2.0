using AutoMapper;
using Data;
using Data.Decids;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.Decids;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecidsController : ControllerBase
    {
        private readonly IDecidsApiRepo _repository;
        private readonly IMapper _mapper;

        public DecidsController(IDecidsApiRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Decids
        [HttpGet]
        public ActionResult<IEnumerable<DecidReadDto>> GetDecid()
        {
            var decids = _repository.GetAllDecids();
            return Ok(_mapper.Map<IEnumerable<DecidReadDto>>(decids));
        }

        // GET: api/Decids/5
        [HttpGet("{id}", Name = "GetDecid")]
        public ActionResult<DecidReadDto> GetDecid(string id)
        {
            var decid = _repository.GetDecid(id);

            if (decid == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DecidReadDto>(decid));
        }

        [HttpPost]
        public ActionResult<DecidReadDto> CreateDecid(DecidCreateDto decidCreateDto)
        {
            var decidModel = _mapper.Map<Decid>(decidCreateDto);
            _repository.CreateDecid(decidModel);
            _repository.SaveChanges();
            var decidReadDto = _mapper.Map<DecidReadDto>(decidModel);
            return CreatedAtRoute(nameof(GetDecid),
            new { Id = decidReadDto.IdDecid }, decidReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateDecid(string id, DecidUpdateDto decidUpdateDto)
        {
            var decidModelFromRepo = _repository.GetDecid(id);
            if (decidModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(decidUpdateDto, decidModelFromRepo);
            _repository.UpdateDecid(decidModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialDecidUpdate(string id, JsonPatchDocument<DecidUpdateDto> patchDoc)
        {
            var decidModelFromRepo = _repository.GetDecid(id);
            if (decidModelFromRepo == null)
            {
                return NotFound();
            }
            var decidToPatch = _mapper.Map<DecidUpdateDto>(decidModelFromRepo);
            patchDoc.ApplyTo(decidToPatch, ModelState);
            if (!TryValidateModel(decidToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(decidToPatch, decidModelFromRepo);
            _repository.UpdateDecid(decidModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDecid(string id)
        {
            var decidModelFromRepo = _repository.GetDecid(id);
            if (decidModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteDecid(decidModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}