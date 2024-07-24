using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.V1.Dtos
{
    public class DisciplinaDto
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public int? PreRequesitoId { get; set; } = null;
        public DisciplinaDto PreRequesito { get; set; }
        public int ProfessorId { get; set; }
        public ProfessorDto Professor { get; set; }
        public int CursoId { get; set; }
        public CursoDto Curso { get; set; }
        public IEnumerable<AlunoDto> Alunos { get; set; }
    }
}