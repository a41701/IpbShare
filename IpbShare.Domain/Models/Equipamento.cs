using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpbShare.Domain.Models
{
    public class Equipamento : Entity
    {
        public string NomeEquipamento { get; set; }
        public string DescricaoEquipamento { get; set; }
        public bool IsReservado { get; set; }
        public byte[] Thumb { get; set; }

        //FK
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        //FK
        public int EscolaId { get; set; }
        public Escola Escola { get; set; }


        public List<Reserva> Reservados { get; set; }

        public Equipamento()
        {
            
        }
        public Equipamento(string n)
        {
            NomeEquipamento = n;
        }

        public Equipamento(int id)
        {
            Id = id;
        }
    }
}