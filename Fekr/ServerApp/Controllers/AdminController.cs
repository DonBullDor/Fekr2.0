using System.Collections.Generic;
using AutoMapper;
using Data.Admins;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.Decids;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminApiRepo _repository;

        private readonly IMapper _mapper;

        public AdminController(IAdminApiRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Decids
        [HttpGet]
        public ActionResult<IEnumerable<AdminReadDto>> GetAllAdmins()
        {
            var admins = _repository.GetAllDecids();
            return Ok(_mapper.Map<IEnumerable<AdminReadDto>>(admins));
        }

        // GET: api/Decids/5
        [HttpGet("{id}", Name = "GetAdmin")]
        public ActionResult<AdminReadDto> GetAdmin(string id)
        {
            var admin = _repository.GetDecid(id);

            if (admin == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AdminReadDto>(admin));
        }

        [HttpPost]
        public ActionResult<AdminReadDto>
        CreateAdmin(AdminCreateDto adminCreateDto)
        {
            var adminModel = _mapper.Map<Decid>(adminCreateDto);
            _repository.CreateDecid(adminModel);
            _repository.SaveChanges();
            var adminReadDto = _mapper.Map<AdminReadDto>(adminModel);
            return CreatedAtRoute(nameof(GetAdmin),
            new { Id = adminReadDto.IdDecid },
            adminReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult
        UpdateAdmin(string id, AdminUpdateDto adminUpdateDto)
        {
            var adminModelFromRepo = _repository.GetDecid(id);
            if (adminModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map (adminUpdateDto, adminModelFromRepo);
            _repository.UpdateDecid (adminModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult
        PartialAdminUpdate(
            string id,
            JsonPatchDocument<AdminUpdateDto> patchDoc
        )
        {
            var adminModelFromRepo = _repository.GetDecid(id);
            if (adminModelFromRepo == null)
            {
                return NotFound();
            }
            var adminToPatch = _mapper.Map<AdminUpdateDto>(adminModelFromRepo);
            patchDoc.ApplyTo (adminToPatch, ModelState);
            if (!TryValidateModel(adminToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map (adminToPatch, adminModelFromRepo);
            _repository.UpdateDecid (adminModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAdmin(string id)
        {
            var adminModelFromRepo = _repository.GetDecid(id);
            if (adminModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteDecid (adminModelFromRepo);
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
