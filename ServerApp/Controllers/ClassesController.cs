using AutoMapper;
using Data;
using Data.Classes;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.Classes;
using System.Collections.Generic;

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
        public ActionResult<IEnumerable<ClasseReadDto>> GetClasse()
        {
            var classes = _repository.GetAllClasses();
            return Ok(_mapper.Map<IEnumerable<ClasseReadDto>>(classes));
        }

        // GET: api/ClassesApi/5
        [HttpGet("{id}", Name = "GetClasse")]
        public ActionResult<ClasseReadDto> GetClasse(string id)
        {
            var classe = _repository.GetClasse(id);

            if (classe == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ClasseReadDto>(classe));
        }

        // POST: api/ClassesApi
        [HttpPost]
        public ActionResult<ClasseReadDto> CreateClasse(ClasseCreateDto classeCreateDto)
        {
            var classeModel = _mapper.Map<Classe>(classeCreateDto);
            _repository.CreateClasse(classeModel);
            _repository.SaveChanges();
            var classeReadDto = _mapper.Map<ClasseReadDto>(classeModel);
            return CreatedAtRoute(nameof(GetClasse),
            new { Id = classeReadDto.CodeCl }, classeReadDto);

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