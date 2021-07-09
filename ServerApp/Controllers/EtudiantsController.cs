using AutoMapper;
using Data;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Repository;
using System.Collections.Generic;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtudiantsController : ControllerBase
    {
        private readonly IEtudiantApiRepo _repository;
        private readonly IMapper _mapper;

        public EtudiantsController(IEtudiantApiRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/EtudiantsApi
        [HttpGet]
        public ActionResult<IEnumerable<EtudiantDto>> GetAllEspEtudiant()
        {
            var etudiants = _repository.GetAllEspEtudiant();
            return Ok(_mapper.Map<IEnumerable<EtudiantDto>>(etudiants));
        }

        // GET: api/EtudiantsApi/5
        [HttpGet("{id}")]
        public ActionResult<EtudiantDto> GetEspEtudiant(string id)
        {
            var espEtudiant = _repository.GetEspEtudiant(id);
            if (espEtudiant == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ClasseDto>(espEtudiant));
        }

        //    // PUT: api/EtudiantsApi/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutEspEtudiant(string id, EspEtudiant espEtudiant)
        //    {
        //        if (id != espEtudiant.IdEt)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(espEtudiant).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EspEtudiantExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // POST: api/EtudiantsApi
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<EspEtudiant>> PostEspEtudiant(EspEtudiant espEtudiant)
        //    {
        //        _context.EspEtudiant.Add(espEtudiant);
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException)
        //        {
        //            if (EspEtudiantExists(espEtudiant.IdEt))
        //            {
        //                return Conflict();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return CreatedAtAction("GetEspEtudiant", new { id = espEtudiant.IdEt }, espEtudiant);
        //    }

        //    // DELETE: api/EtudiantsApi/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteEspEtudiant(string id)
        //    {
        //        var espEtudiant = await _context.EspEtudiant.FindAsync(id);
        //        if (espEtudiant == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.EspEtudiant.Remove(espEtudiant);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool EspEtudiantExists(string id)
        //    {
        //        return _context.EspEtudiant.Any(e => e.IdEt == id);
        //    }
    }
}