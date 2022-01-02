using IpbShare.Domain.Models;
using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Domain.Repositories
{
    public interface ICursoRepository : IRepository<Curso>
    {
        Task<Curso> FindByNameAsync( string name);

        Task<List<Curso>> FindAllByNameStartWithAsync(string name);

    }
}
