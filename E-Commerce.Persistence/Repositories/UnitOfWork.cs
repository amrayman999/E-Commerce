using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Context;

namespace E_Commerce.Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext context) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : Entity<TKey>
        {
            var entityName = typeof(TEntity).Name;

            if (_repositories.TryGetValue(entityName, out object? value))
                 return (value as IRepository<TEntity, TKey>)!;

            var repository = new Repository<TEntity, TKey>(context);

            _repositories.Add(entityName, repository);
            return repository;
        }

        public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
    }
}
