using IpbShare.Domain.Models;
using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Domain.Repositories
{
    public interface IPaisRepository: IRepository<Pais>
    {
        Task<Pais> FindByNameAsync(string name);

        Task<List<Pais>> FindAllByNameStartWithAsync(string name);
    }
}
