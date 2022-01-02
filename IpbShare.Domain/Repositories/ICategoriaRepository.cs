using IpbShare.Domain.Models;
using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Domain.Repositories
{
    public interface ICategoriaRepository : IRepository <Categoria> 
    {
        Task<Categoria> FindByNameAsync(string name);
        Task<List<Categoria>> FindAllByNameStartWithAsync(string name);
    }
}
