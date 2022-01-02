using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpbShare.Domain.Models
{
    public class Pais : Entity
    {
        public string NomePais { get; set; }

        public List<Utilizador> UtilizadorList { get; set; }

        public Pais()
        {

        }

        public Pais(string nome)
        {
            NomePais = nome;
        }

        public Pais(int paisId)
        {
            Id = paisId;
        }
    }
}
