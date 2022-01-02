using IpbShare.Domain.Models;
using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Domain.Repositories
{
    public interface IEquipamentoRepository: IRepository<Equipamento>
    {
        Task<Equipamento> FindByNameAsync(string name);
        Task<List<Equipamento>> FindAllByNameStartWithAsync(string name);
    }
}
