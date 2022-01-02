using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Domain.SeedWork
{
    public interface IRepository<T> where T: Entity
    {
        T Create(T e);
        T Update(T e);

        Task<T> UpsertAsync(T e);
        Task<T> FindOrCreatAsync(T e);

        T Delete(T a);
        Task<T> FindByIdAsync(int id);
        Task<List<T>> FindAllAsync();
    }
}

