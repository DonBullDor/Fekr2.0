﻿using AutoMapper;
using Data;
using Data.Decids;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Repository.Decids;
using System.Collections.Generic;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecidsController : ControllerBase
    {
        private readonly IDecidsApiRepo _repository;
        private readonly IMapper _mapper;

        public DecidsController(IDecidsApiRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Decids
        [HttpGet]
        public ActionResult<IEnumerable<DecidReadDto>> GetDecid()
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
        public ActionResult<DecidReadDto> CreateDecid(DecidCreateDto decidCreateDto)
        {
            var decidModel = _mapper.Map<Decid>(decidCreateDto);
            _repository.CreateDecid(decidModel);
            _repository.SaveChanges();
            var decidReadDto = _mapper.Map<DecidReadDto>(decidModel);
            return CreatedAtRoute(nameof(GetDecid),
            new { Id = decidReadDto.IdDecid }, decidReadDto);
        }
        //// PUT: api/Decids/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDecid(string id, Decid decid)
        //{
        //    if (id != decid.IdDecid)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(decid).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DecidExists(id))
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

        //// DELETE: api/Decids/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDecid(string id)
        //{
        //    var decid = await _context.Decid.FindAsync(id);
        //    if (decid == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Decid.Remove(decid);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool DecidExists(string id)
        //{
        //    return _context.Decid.Any(e => e.IdDecid == id);
        //}
    }
}