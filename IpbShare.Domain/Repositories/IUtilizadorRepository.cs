using IpbShare.Domain.Models;
using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Domain.Repositories
{
    public interface IUtilizadorRepository : IRepository<Utilizador>
    {
        Task<Utilizador> FindByEmailAsync(string UserEmail);
        Task<List<Utilizador>> FindAllByNameStartWithAsync(string name);
    }
}