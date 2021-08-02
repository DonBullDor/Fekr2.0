using System.Collections.Generic;
using AutoMapper;
using Data;
using Data.Decids;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using ServerApp.Services;
using ServerApp.Helpers.Parent;
using Service.Repository.Decids;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private IParentLoginService _service;

        public ParentController(IParentLoginService service)
        {
            _service = service;
        }

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

        [ParentAuthorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _service.GetAll();
            return Ok(users);
        }
    }
}
