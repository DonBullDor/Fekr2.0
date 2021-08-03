using System.Collections.Generic;
using AutoMapper;
using Data;
using Data.Decids;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Helpers.Admin;
using ServerApp.Models;
using ServerApp.Services;
using Service.Repository.Decids;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecidsController : ControllerBase
    {
        private readonly IDecidsApiRepo _repository;

        

        private readonly IMapper _mapper;

        public DecidsController(
            IDecidsApiRepo repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
           
        }

        // GET: api/Decids
        [HttpGet]
        public ActionResult<IEnumerable<DecidReadDto>> GetAllDecids()
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
        public ActionResult<DecidReadDto>
        CreateDecid(DecidCreateDto decidCreateDto)
        {
            var decidModel = _mapper.Map<Decid>(decidCreateDto);
            _repository.CreateDecid (decidModel);
            _repository.SaveChanges();
            var decidReadDto = _mapper.Map<DecidReadDto>(decidModel);
            return CreatedAtRoute(nameof(GetDecid),
            new { Id = decidReadDto.IdDecid },
            decidReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult
        UpdateDecid(string id, DecidUpdateDto decidUpdateDto)
        {
            var decidModelFromRepo = _repository.GetDecid(id);
            if (decidModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map (decidUpdateDto, decidModelFromRepo);
            _repository.UpdateDecid (decidModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult
        PartialDecidUpdate(
            string id,
            JsonPatchDocument<DecidUpdateDto> patchDoc
        )
        {
            var decidModelFromRepo = _repository.GetDecid(id);
            if (decidModelFromRepo == null)
            {
                return NotFound();
            }
            var decidToPatch = _mapper.Map<DecidUpdateDto>(decidModelFromRepo);
            patchDoc.ApplyTo (decidToPatch, ModelState);
            if (!TryValidateModel(decidToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map (decidToPatch, decidModelFromRepo);
            _repository.UpdateDecid (decidModelFromRepo);
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
            _repository.DeleteDecid (decidModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
 /*
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _service.Authenticate(model);

            if (response == null)
                return BadRequest(new {
                    message = "Username or password is incorrect"
                });

            return Ok(response);
        }

        [AdminAuthorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _service.GetAll();
            return Ok(users);
        }
  */      
    }
}
