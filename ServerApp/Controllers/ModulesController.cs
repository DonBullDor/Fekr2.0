using AutoMapper;
using Data;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.Modules;
using System.Collections.Generic;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleApiRepo _repository;
        private readonly IMapper _mapper;

        public ModulesController(IModuleApiRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/ModulesApi
        [HttpGet]
        public ActionResult<IEnumerable<ModuleDto>> GetEspModule()
        {
            var modules = _repository.GetAllModules();
            return Ok(_mapper.Map<IEnumerable<ModuleDto>>(modules));
        }

        // GET: api/ModulesApi/5
        [HttpGet("{id}")]
        public ActionResult<ModuleDto> GetEspModule(string id)
        {
            var espModule = _repository.GetModule(id);

            if (espModule == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ClasseDto>(espModule));
        }

        //// PUT: api/ModulesApi/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEspModule(string id, EspModule espModule)
        //{
        //    if (id != espModule.CodeModule)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(espModule).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EspModuleExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/ModulesApi
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<EspModule>> PostEspModule(EspModule espModule)
        //{
        //    _context.EspModule.Add(espModule);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (EspModuleExists(espModule.CodeModule))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetEspModule", new { id = espModule.CodeModule }, espModule);
        //}

        //// DELETE: api/ModulesApi/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEspModule(string id)
        //{
        //    var espModule = await _context.EspModule.FindAsync(id);
        //    if (espModule == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.EspModule.Remove(espModule);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool EspModuleExists(string id)
        //{
        //    return _context.EspModule.Any(e => e.CodeModule == id);
        //}
    }
}