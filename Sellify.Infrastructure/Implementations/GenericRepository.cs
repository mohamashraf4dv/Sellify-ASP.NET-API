
using Sellify.Application.Contracts.Repositories;
using Sellify.Domain.Contracts;

namespace Sellify.Infrastructure.Implementations
{
    public class GenericRepository<TEntity> :IGenericRepository<TEntity> where TEntity : class, ISoftDeletable, IEntityUpdatable, new()
    {
        private readonly SellifyMicrosoftSqlContext _context;

        public GenericRepository(SellifyMicrosoftSqlContext context)
        {
            this._context = context;
        }

        public async Task<TEntity> CreateAsync(TEntity entity,CancellationToken cancellationToken)
        {
             await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            entity.LastUpdatedAt= DateTime.UtcNow;
            _context.Set<TEntity>().Update(entity);
            return entity;
        }

        public TEntity Delete(TEntity entity) 
        {
            entity.DeletedAt = DateTime.UtcNow;
            entity.IsDeleted = true;
            _context.Set<TEntity>().Update(entity);
            return entity;
        }

    }
}
