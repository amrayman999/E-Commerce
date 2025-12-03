using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Persistence.Repositories
{
    public class Repository<TEntity, TKey>(StoreDbContext context) : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
        public async Task AddAsync(TEntity entity) => await context.Set<TEntity>().AddAsync(entity);

        public async Task<int> CountAsync(ISpecification<TEntity> specification)
            => await _dbSet.ApplySpecification(specification).CountAsync();


        public async Task<IEnumerable<TEntity>> GetAllAsync() => await context.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification) => 
                   await _dbSet.ApplySpecification(specification).ToListAsync();

        public async Task<TEntity>? GetAsync(ISpecification<TEntity> specification) => 
            await _dbSet.ApplySpecification(specification).FirstOrDefaultAsync();
        public async Task<TEntity>? GetByIdAsync(TKey id) => await context.Set<TEntity>().FindAsync(id);

        public void Remove(TEntity entity) => context.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => context.Set<TEntity>().Update(entity);
    }
}
