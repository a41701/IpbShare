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
	public class UtilizadorRepository : Repository<Utilizador>, IUtilizadorRepository
	{

		public UtilizadorRepository(IpbShareDbContext dbContext) : base(dbContext) //construtor por defeito
		{

		}

		public Task<Utilizador> FindByEmailAsync(string UserEmail)
		{
			return _dbContext.Utilizadores.SingleOrDefaultAsync(u => u.Email == UserEmail);
		}

		public override async Task<Utilizador> FindOrCreatAsync(Utilizador e)
		{
			var utilizador = await _dbContext.Utilizadores.SingleOrDefaultAsync(u => u.Id == e.Id);

			if (utilizador == null)
			{
				utilizador = Create(e);
				await _dbContext.SaveChangesAsync();
			}

			return utilizador;
		}

		public override async Task<Utilizador> UpsertAsync(Utilizador e)
		{
			Utilizador utilizador = null;
			Utilizador existing = await FindByIdAsync(e.Id);

			if (existing == null)
			{
				if (e.Id == 0)
				{
					utilizador = Create(e);
				}
				else
				{
					utilizador = Update(e);
				}

			}
			else if (existing.Id == e.Id)
			{
				// Prevent Detached State before Update??

				utilizador = Update(e);
			}
			else
			{
				_dbContext.Entry(e).State = EntityState.Detached;
			}

			await _dbContext.SaveChangesAsync();

			return utilizador;
		}

		#region extra methods

		//método administrativo
		public override async Task<List<Utilizador>> FindAllAsync()
		{
			return await _dbContext.Utilizadores
				.Include(u => u.Curso)
				.Include(u => u.Pais)
				.OrderBy(u => u.Nome)
				.ToListAsync();

		}

		public Task<List<Utilizador>> FindAllByNameStartWithAsync(string name)
		{
			return _dbContext.Utilizadores
				.Where(p => p.Nome.StartsWith(name))
				.OrderBy(p => p.Nome)
				.ToListAsync();
		}

		#endregion
	}
}
