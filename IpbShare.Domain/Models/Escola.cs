using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpbShare.Domain.Models
{
    public class Escola : Entity
    {
        public string NomeEscola { get; set; }

        public List<Curso> CursoList { get; set; }

        public List<Equipamento> Equipamentos { get; set; }

        public Escola()
        {

        }
        public Escola(string n)
        {
            NomeEscola = n;
        }

        public Escola(int EscolaId)
        {
            Id = EscolaId;
        }
    }
}
