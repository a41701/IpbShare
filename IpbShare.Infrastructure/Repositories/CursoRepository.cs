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
	public class CursoRepository : Repository<Curso>, ICursoRepository
	{
        public CursoRepository(IpbShareDbContext dbContext) : base(dbContext) //construtor por defeito
		{

		}

		public Task<Curso> FindByNameAsync(string name)
		{
			return _dbContext.Cursos.SingleOrDefaultAsync(c => c.NomeCurso == name);
		}

		public override async Task<Curso> FindOrCreatAsync(Curso e)
		{
			var curso = await _dbContext.Cursos.SingleOrDefaultAsync(c => c.NomeCurso == e.NomeCurso);

			if (curso == null)
			{
				curso = Create(e);
				await _dbContext.SaveChangesAsync();
			}

			return curso;
		}

		public override async Task<Curso> UpsertAsync(Curso e)
		{
			Curso curso = null;
			Curso existing = await FindByNameAsync(e.NomeCurso);

			if (existing == null)
			{
				if (e.Id == 0)
				{
					curso = Create(e);
				}
				else
				{
					curso = Update(e);
				}

			}
			else if (existing.Id == e.Id)
			{
				// Prevent Detached State before Update??

				curso = Update(e);
			}
			else
			{
				_dbContext.Entry(e).State = EntityState.Detached;
			}

			await _dbContext.SaveChangesAsync();

			return curso;
		}

		#region extra methods

		//método administrativo
		public override async Task<List<Curso>> FindAllAsync()
		{
			return await _dbContext.Cursos
				.Include(c => c.UtilizadorList)
				.Include(c => c.Escola)
				.OrderBy(c => c.NomeCurso)
				.ToListAsync();
		}

		public Task<List<Curso>> FindAllByNameStartWithAsync(string name)
        {
			return _dbContext.Cursos
			   .Where(c => c.NomeCurso.StartsWith(name))
			   .OrderBy(c => c.NomeCurso)
			   .ToListAsync();
		}

		#endregion

	}
}

