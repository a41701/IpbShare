using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpbShare.Domain.Models
{
    public class Curso : Entity
    {
        public string NomeCurso { get; set; }
        public int NumAlunos { get; set; }

        //FK
        public int EscolaId { get; set; }
        public Escola Escola { get; set; }


        public List<Utilizador> UtilizadorList { get; set; }

        public Curso()
        {

        }

        public Curso(string n)
        {
            NomeCurso = n;
        }

        public Curso (int idCurso)
        {
            Id = idCurso;
        }
    }
}
