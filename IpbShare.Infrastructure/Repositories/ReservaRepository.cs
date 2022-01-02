using IpbShare.Domain.Models;
using IpbShare.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Infrastructure.Repositories
{
	public class ReservaRepository : Repository<Reserva>, IReservaRepository
	{

		public ReservaRepository(IpbShareDbContext dbContext) : base(dbContext) //construtor por defeito
		{

		}
        //ver ultimo equipamento requisitado por um user
        public Reserva FindLastProductRequisitedByUser(int userId)
        {
            return _dbContext.Reservas
                .Where(r => r.UserId == userId)
                .LastOrDefault();
        }


        //ver equipamentos requisitados por um user
        public Task<List<Reserva>> FindProductsTakeByUserAsync(int userId)
        {   
            return _dbContext.Reservas
                .Include(r => r.Equipamento) //incluir equipamento para incluir o IsReservado
                .Where(r => r.UserId == userId && r.Equipamento.IsReservado)
                .OrderBy(r => r.DataReserva)//ordena por data de reserva
                .ToListAsync();
        }
         //ver historico dum user
        public Task<List<Reserva>> HistoricAsync(DateTime date)
        {
            return _dbContext.Reservas
            .Where(r => r.DataReserva == date)
            .OrderBy(r => r.DataReserva)
            .ToListAsync();
        }

      
        //ver historico de reservas dum user
        public Task<List<Reserva>> UserHistoricAsync(int userId)
        {
            return _dbContext.Reservas
            .Where(r=> r.UserId == userId)
            .OrderBy(r => r.DataReserva)
            .ToListAsync();
        }
        }
    }