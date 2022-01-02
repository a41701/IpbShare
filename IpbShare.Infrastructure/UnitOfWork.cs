using IpbShare.Domain;
using IpbShare.Domain.Repositories;
using IpbShare.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IpbShareDbContext _dbContext;

        public ICategoriaRepository CategoriaRepository => new CategoriaRepository(_dbContext);

        public ICursoRepository CursoRepository => new CursoRepository(_dbContext);

        public IEquipamentoRepository EquipamentoRepository => new EquipamentoRepository(_dbContext);

        public IEscolaRepository EscolaRepository => new EscolaRepository(_dbContext);

        public IPaisRepository PaisRepository => new PaisRepository(_dbContext);

        public IReservaRepository ReservaRepository => new ReservaRepository(_dbContext);

        public IUtilizadorRepository UtilizadorRepository => new UtilizadorRepository(_dbContext);

        public UnitOfWork()
        {
            _dbContext = new IpbShareDbContext();
            _dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _dbContext.Dispose(); //liberta a memória alocada à variável
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}