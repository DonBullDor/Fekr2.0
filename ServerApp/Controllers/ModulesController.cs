using AutoMapper;
using Data;
using Data.Module;
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
        public ActionResult<IEnumerable<ModuleReadDto>> GetEspModule()
        {
            var modules = _repository.GetAllModules();
            return Ok(_mapper.Map<IEnumerable<ModuleReadDto>>(modules));
        }

        // GET: api/ModulesApi/5
        [HttpGet("{id}", Name = "GetModule")]
        public ActionResult<ModuleReadDto> GetModule(string id)
        {
            var espModule = _repository.GetModule(id);

            if (espModule == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ModuleReadDto>(espModule));
        }

        [HttpPost]
        public ActionResult<ModuleReadDto> CreateModule(ModuleCreateDto moduleCreateDto)
        {
            var moduleModel = _mapper.Map<EspModule>(moduleCreateDto);
            _repository.CreateModule(moduleModel);
            _repository.SaveChanges();
            var moduleReadDto = _mapper.Map<ClasseReadDto>(moduleModel);
            return CreatedAtRoute(nameof(GetModule),
            new { Id = moduleReadDto.CodeCl }, moduleReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateModule(string id, ModuleUpdateDto moduleUpdateDto)
        {
            var moduleModelFromRepo = _repository.GetModule(id);
            if (moduleModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(moduleUpdateDto, moduleModelFromRepo);
            _repository.UpdateModule(moduleModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
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