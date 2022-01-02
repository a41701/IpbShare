using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpbShare.Domain.Models
{
    public class Reserva : Entity
    {
        public DateTime DataReserva { get; set;}
        public DateTime DataEntrega { get; set; }

        //FK 
        public int EquipamentoId { get; set;}
        public Equipamento Equipamento { get; set; }

        //FK 
        public int UserId { get; set; }
        public Utilizador Utilizador { get; set; }

        public Reserva()
        {

        }

        //TODO : Verificar se é mesmo necessário
        public Reserva(int idReserva)
        {
            Id = idReserva;

        }
    }
}
