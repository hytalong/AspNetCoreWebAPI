using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _context;
        public ProfessorController(SmartContext context) 
        { 
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var Professor = _context.Professores.FirstOrDefault(a => a.Id == id);
            if (Professor == null) return BadRequest("O professor não foi encontrado");
            
            return Ok(Professor);
        }

        [HttpGet("ByName")]
        public IActionResult GetByName(string nome)
        {
            var Professor = _context.Professores.FirstOrDefault(a => 
                a.Nome.Contains(nome)
                );
            if (Professor == null) return BadRequest("O professor não foi encontrado");
            
            return Ok(Professor);
        }


        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(prof == null) return BadRequest("Aluno não encontrado");

            
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(prof == null) return BadRequest("Aluno não encontrado");

            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Professor = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(Professor == null) return BadRequest("Aluno não encontrado");

            _context.Remove(Professor);
            _context.SaveChanges();
            return Ok();
        }
    }
}
