using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Models;
using  SmartSchool.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.V1.Dtos;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.V2.Controllers
{
    /// <summary>
    /// Versão 2 do meu controlador de Aluno
    /// </summary>
    
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IRepository _repo;
        public AlunoController(IRepository repo, IMapper mapper) 
        {
            _mapper = mapper;
            _repo = repo;
        }
        /// <summary>
        /// Método responsável por retornar todos os alunos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            
            var alunos = _repo.GetAllAlunos(true);
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }
        
        /// <summary>
        /// Método responsável por retornar apenas um Aluno por meio do Código ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");
            
            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }

        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if(_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não caastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id);
            if(aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);
            
            _repo.Update(aluno);
            if(_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não caastrado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id);
            if(aluno == null) return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);
            if(_repo.SaveChanges())
            {
                return Ok("Aluno deletado");
            }
            return BadRequest("Aluno não deletado");
        }
    }
}
