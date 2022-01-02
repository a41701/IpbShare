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
	public class PaisRepository : Repository<Pais>, IPaisRepository
	{

		public PaisRepository(IpbShareDbContext dbContext) : base(dbContext) //construtor por defeito
		{

		}

		public Task<Pais> FindByNameAsync(string name)
		{
			return _dbContext.Paises.SingleOrDefaultAsync(p => p.NomePais == name);
		}

		public override async System.Threading.Tasks.Task<Pais> FindOrCreatAsync(Pais e)
		{
			var pais = await _dbContext.Paises.SingleOrDefaultAsync(p => p.NomePais == e.NomePais);

			if (pais == null)
			{
				pais = Create(e);
				await _dbContext.SaveChangesAsync();
			}

			return pais;
		}

		public override async System.Threading.Tasks.Task<Pais> UpsertAsync(Pais e)
		{
			Pais pais = null;
			Pais existing = await FindByNameAsync(e.NomePais);

			if (existing == null)
			{
				if (e.Id == 0)
				{
					pais = Create(e);
				}
				else
				{
					pais = Update(e);
				}

			}
			else if (existing.Id == e.Id)
			{
				// Prevent Detached State before Update??

				pais = Update(e);
			}
			else
			{
				_dbContext.Entry(e).State = EntityState.Detached;
			}

			await _dbContext.SaveChangesAsync();

			return pais;
		}

        #region extra methods

		public Task<List<Pais>> FindAllByNameStartWithAsync(string name)
        {
			return _dbContext.Paises
				.Where(p => p.NomePais.StartsWith(name))
				.OrderBy(p => p.NomePais)
				.ToListAsync();
		}

		#endregion
	}
}
