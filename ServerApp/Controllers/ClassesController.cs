using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.Classes;
using System.Collections.Generic;
using AutoMapper;
using Data;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassesApiRepo _repository;
        private readonly IMapper _mapper;

        public ClassesController(IClassesApiRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/ClassesApi
        [HttpGet]
        public ActionResult<IEnumerable<ClasseDto>> GetClasse()
        {
            var classes = _repository.GetAllClasses();
            return Ok(_mapper.Map<IEnumerable<ClasseDto>>(classes));
        }

        // GET: api/ClassesApi/5
        [HttpGet("{id}")]
        public ActionResult<ClasseDto> GetClasse(string id)
        {
            var classe = _repository.GetClasse(id);

            if (classe == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ClasseDto>(classe));
        }

        //// PUT: api/ClassesApi/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutClasse(string id, Classe classe)
        //{
        //    if (id != classe.CodeCl)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(classe).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ClasseExists(id))
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

        //// POST: api/ClassesApi
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Classe>> PostClasse(Classe classe)
        //{
        //    _context.Classe.Add(classe);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ClasseExists(classe.CodeCl))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetClasse", new { id = classe.CodeCl }, classe);
        //}

        //// DELETE: api/ClassesApi/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteClasse(string id)
        //{
        //    var classe = await _context.Classe.FindAsync(id);
        //    if (classe == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Classe.Remove(classe);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ClasseExists(string id)
        //{
        //    return _context.Classe.Any(e => e.CodeCl == id);
        //}
    }
}