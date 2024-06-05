using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SmartSchool.WebAPI.V1.Dtos;

namespace SmartSchool.API.V1.Controllers
{
    /// <summary>
    /// Versão 2 do meu controlador de Professores
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        public ProfessorController(IRepository repo, IMapper mapper) 
        { 
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(result));
        }

        [HttpGet("getRegister")]
        public IActionResult getRegister()
        {
            return Ok(new ProfessorRegistrarDto());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Professor = _repo.GetProfessoresById(id, false);
            if (Professor == null) return BadRequest("O professor não foi encontrado");

            var professorDto = _mapper.Map<ProfessorDto>(Professor);
            
            return Ok(Professor);
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repo.Add(professor);
            if(_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não caastrado");
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessoresById(id, false);
            if(professor == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, professor);
            
            _repo.Update(professor);
            if(_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessoresById(id, false);
            if(professor == null) return BadRequest("Aluno não encontrado");


            _mapper.Map(model, professor);

            _repo.Update(professor);
            if(_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = _repo.GetProfessoresById(id, false);
            if(prof == null) return BadRequest("Aluno não encontrado");

            _repo.Delete(prof);
            if(_repo.SaveChanges())
            {
                return Ok("Professor deletado");
            }
            return BadRequest("Professor não deletado");
        }
    }
}
