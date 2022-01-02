using Microsoft.EntityFrameworkCore;
using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Infrastructure.Repositories
{
        public abstract class Repository<T> : IRepository<T> where T : Entity
        {
            public Repository(IpbShareDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            protected readonly IpbShareDbContext _dbContext;

            public T Create(T e)
            {
                T entity = _dbContext.Set<T>().Add(e).Entity; //criar um novo registo na BD
                return entity;
            }

            public T Delete(T e)
            {
                T entity = _dbContext.Set<T>().Remove(e).Entity;
                return entity;
            }

            public virtual async Task<List<T>> FindAllAsync()
            {
                return await _dbContext.Set<T>().ToListAsync();
            }

            public async Task<T> FindByIdAsync(int id)
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }

            //TODO: mostrar isto ao stor
            public async virtual Task<T> FindOrCreatAsync(T e)
            {
            var entity = await _dbContext.Set<T>().SingleOrDefaultAsync(T => T.Id == e.Id);

                if (entity == null)
                {
                    entity = Create(e);
                    await _dbContext.SaveChangesAsync();
                }
                return entity;
            }

            public T Update(T e)
            {
                _dbContext.Entry(e).State = EntityState.Modified; //modificar o State de uma entidade
                T entity = _dbContext.Set<T>().Update(e).Entity; //fazer o update à entidade
                return entity;
            }
            
            //TODO: mostrar isto ao stor
            public async virtual Task<T> UpsertAsync(T e)
            {
                T entity = null;
                T existing = await FindByIdAsync(e.Id);

                if (existing == null)
                {
                    if (e.Id == 0)
                    {
                        entity = Create(e);
                    }
                    else
                    {
                        entity = Update(e);
                    }

                }
                else if (existing.Id == e.Id)
                {
                    // Prevent Detached State before Update??

                    entity = Update(e);
                }
                else
                {
                    _dbContext.Entry(e).State = EntityState.Detached;
                }

                await _dbContext.SaveChangesAsync();

                return entity;
            }
        }
    }
