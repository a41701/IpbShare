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
	public class EscolaRepository : Repository<Escola>, IEscolaRepository
	{
		public EscolaRepository(IpbShareDbContext dbContext) : base(dbContext) //construtor por defeito
		{

		}

		public Task<Escola> FindByNameAsync(string name)
		{
			return _dbContext.Escolas.SingleOrDefaultAsync(e => e.NomeEscola == name);
		}

		public override async Task<Escola> FindOrCreatAsync(Escola es)
		{
			var escola = await _dbContext.Escolas.SingleOrDefaultAsync(e => e.NomeEscola == es.NomeEscola);

			if (escola == null)
			{
				escola = Create(es);
				await _dbContext.SaveChangesAsync();
			}

			return escola;
		}

		public override async Task<Escola> UpsertAsync(Escola e)
		{
			Escola escola = null;
			Escola existing = await FindByNameAsync(e.NomeEscola);

			if (existing == null)
			{
				if (e.Id == 0)
				{
					escola = Create(e);
				}
				else
				{
					escola = Update(e);
				}

			}
			else if (existing.Id == e.Id)
			{
				// Prevent Detached State before Update??

				escola = Update(e);
			}
			else
			{
				_dbContext.Entry(e).State = EntityState.Detached;
			}

			await _dbContext.SaveChangesAsync();

			return escola;
		}

		#region extra methods

		public Task<List<Escola>> FindAllByNameStartWithAsync(string name)
        {
			return _dbContext.Escolas
				.Where(e => e.NomeEscola.StartsWith(name))
				.OrderBy(e => e.NomeEscola)
				.ToListAsync();
		}

		#endregion
	}
}
