using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpbShare.Domain.Models
{
    public class Categoria : Entity
    {
        public string NomeCategoria { get; set;}
        public string NomePais { get; set; }
        public List<Equipamento> Equipamentos { get; set; }

        public Categoria()
        {

        }

        public Categoria(string n)
        {
            NomeCategoria = n;
        }
    }
}
