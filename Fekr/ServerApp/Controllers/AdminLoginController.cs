using System.Collections.Generic;
using AutoMapper;
using Data;
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
    public class AdminLoginController : ControllerBase
    {
        private readonly IAdminLoginService _service;

        public AdminLoginController(IAdminLoginService service)
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

        [AdminAuthorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _service.GetAll();
            return Ok(users);
        }
    }
}
