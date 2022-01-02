using IpbShare.Domain.Models;
using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Domain.Repositories
{
    public interface IReservaRepository: IRepository<Reserva>
    {
        //ver ultimo equipamento requisitado por um user
        Reserva FindLastProductRequisitedByUser(int userId);

        //ver equipamentos requisitados por um user
        Task<List<Reserva>> FindProductsTakeByUserAsync(int userId);

        //ver historico de reservas dum user
        Task<List<Reserva>> UserHistoricAsync(int userId);

        //ver historico pela data de reserva
        Task<List<Reserva>> HistoricAsync(DateTime date);
    }
}
