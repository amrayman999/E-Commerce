using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<TEntity>? GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
