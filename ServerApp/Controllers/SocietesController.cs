using AutoMapper;
using Data;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.Societes;
using System.Collections.Generic;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocietesController : ControllerBase
    {
        private readonly ISocietesApiRepo _repository;
        private readonly IMapper _mapper;

        public SocietesController(ISocietesApiRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/SocietesApi
        [HttpGet]
        public ActionResult<IEnumerable<SocieteDto>> GetSociete()
        {
            var scociete = _repository.GetAllSocietes();
            return Ok(_mapper.Map<IEnumerable<SocieteDto>>(scociete));
        }

        // GET: api/SocietesApi/5
        [HttpGet("{id}")]
        public ActionResult<SocieteDto> GetSociete(string id)
        {
            var societe = _repository.GetSociete(id);

            if (societe == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ClasseDto>(societe));
        }

        //// PUT: api/SocietesApi/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSociete(string id, Societe societe)
        //{
        //    if (id != societe.CodeSoc)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(societe).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SocieteExists(id))
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

        //// POST: api/SocietesApi
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Societe>> PostSociete(Societe societe)
        //{
        //    _context.Societe.Add(societe);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (SocieteExists(societe.CodeSoc))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetSociete", new { id = societe.CodeSoc }, societe);
        //}

        //// DELETE: api/SocietesApi/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSociete(string id)
        //{
        //    var societe = await _context.Societe.FindAsync(id);
        //    if (societe == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Societe.Remove(societe);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool SocieteExists(string id)
        //{
        //    return _context.Societe.Any(e => e.CodeSoc == id);
        //}
    }
}