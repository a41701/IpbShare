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
	public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
	{
		public CategoriaRepository(IpbShareDbContext dbContext): base(dbContext) //construtor por defeito
		{

		}


		public Task<Categoria> FindByNameAsync(string name)
		{
			return _dbContext.Categorias.SingleOrDefaultAsync(c => c.NomePais == name);

		}

		public override async Task<Categoria> FindOrCreatAsync(Categoria e)
		{
			var category = await _dbContext.Categorias.SingleOrDefaultAsync(c => c.NomePais == e.NomePais);

			if (category == null)
			{
				category = Create(e);
				await _dbContext.SaveChangesAsync();
			}

			return category;
		}

		public override async Task<Categoria> UpsertAsync(Categoria e)
		{
			Categoria category = null;
			Categoria existing = await FindByNameAsync(e.NomePais);

			if (existing == null)
			{
				if (e.Id == 0)
				{
					category = Create(e);
				}
				else
				{
					category = Update(e);
				}

			}
			else if (existing.Id == e.Id)
			{
				// Prevent Detached State before Update??

				category = Update(e);
			}
			else
			{
				_dbContext.Entry(e).State = EntityState.Detached;
			}

			await _dbContext.SaveChangesAsync();

			return category;
		}

		#region extra methods

		public override async Task<List<Categoria>> FindAllAsync()
		{
			return await _dbContext.Categorias
				.Include(c => c.Equipamentos)
				.OrderBy(c => c.NomePais)
				.ToListAsync();

		}

		public Task<List<Categoria>> FindAllByNameStartWithAsync(string name)
		{
			return _dbContext.Categorias
				.Where(c => c.NomePais.StartsWith(name))
				.OrderBy(c => c.NomePais)
				.ToListAsync();
		}

		#endregion
	}
}