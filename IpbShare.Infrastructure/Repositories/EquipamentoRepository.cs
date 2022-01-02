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
	public class EquipamentoRepository : Repository<Equipamento>, IEquipamentoRepository
	{
		public EquipamentoRepository(IpbShareDbContext dbContext) : base(dbContext) //construtor por defeito
		{

		}

		public Task<Equipamento> FindByNameAsync(string name)
		{
			return _dbContext.Equipamentos.SingleOrDefaultAsync(eq => eq.NomeEquipamento == name);
		}

		public override async Task<Equipamento> FindOrCreatAsync(Equipamento e)
		{
			var equipamento = await _dbContext.Equipamentos.SingleOrDefaultAsync(eq => eq.NomeEquipamento == e.NomeEquipamento);

			if (equipamento == null)
			{
				equipamento = Create(e);
				await _dbContext.SaveChangesAsync();
			}

			return equipamento;
		}

		public override async Task<Equipamento> UpsertAsync(Equipamento e)
		{
			Equipamento equipamento = null;
			Equipamento existing = await FindByNameAsync(e.NomeEquipamento);

			if (existing == null)
			{
				if (e.Id == 0)
				{
					equipamento = Create(e);
				}
				else
				{
				    equipamento = Update(e);
				}

			}
			else if (existing.Id == e.Id)
			{
				// Prevent Detached State before Update??

				equipamento = Update(e);
			}
			else
			{
				_dbContext.Entry(e).State = EntityState.Detached;
			}

			await _dbContext.SaveChangesAsync();

			return equipamento;
		}

		#region extra methods

		public override async Task<List<Equipamento>> FindAllAsync()
		{
			return await _dbContext.Equipamentos
				.Include(eq =>eq.Categoria)
				.OrderBy(eq => eq.NomeEquipamento)
				.ToListAsync();

		}

		public Task<List<Equipamento>> FindAllByNameStartWithAsync(string name)
		{
			return _dbContext.Equipamentos
				.Where(eq => eq.NomeEquipamento.StartsWith(name))
				.OrderBy(eq => eq.NomeEquipamento)
				.ToListAsync();
		}

		public async Task<List<Equipamento>> FindAllByCategoryStartWithAsync(int categoryId, string text)
		{
			return await _dbContext.Equipamentos.Where(eq =>
			eq.NomeEquipamento.StartsWith(text) && (eq.CategoriaId == categoryId))
				.OrderBy(eq => eq.NomeEquipamento)
				.ToListAsync();
		}

		public async Task<List<Equipamento>> FindAllBySchoolStartWithAsync(int escolaId, string text)
		{
			return await _dbContext.Equipamentos.Where(eq =>
			eq.NomeEquipamento.StartsWith(text) && (eq.EscolaId == escolaId))
				.OrderBy(eq => eq.NomeEquipamento)
				.ToListAsync();
		}

		#endregion
	}
}
