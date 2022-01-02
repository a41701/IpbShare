using IpbShare.Domain.Models;
using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Domain.Repositories
{
    public interface IEscolaRepository : IRepository <Escola>
    {
        Task<Escola> FindByNameAsync(string name);
        Task<List<Escola>> FindAllByNameStartWithAsync(string name);
    }
}
