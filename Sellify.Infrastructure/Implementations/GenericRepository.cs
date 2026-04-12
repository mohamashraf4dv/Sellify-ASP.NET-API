

using System.Linq.Expressions;

namespace Sellify.Infrastructure.Implementations
{
    public class GenericRepository<TEntity> :IGenericRepository<TEntity> where TEntity : class, ISoftDeletable, IEntityUpdatable
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
        public bool UpdateRange(IReadOnlyList<TEntity> entitities)
        {
            var changedEntity = entitities.Select(entity => { entity.LastUpdatedAt = DateTime.UtcNow; return entity; });
            _context.Set<TEntity>().UpdateRange(changedEntity);
            return true;
        }

        public TEntity Delete(TEntity entity) 
        {
            entity.DeletedAt = DateTime.UtcNow;
            entity.IsDeleted = true;
            _context.Set<TEntity>().Update(entity);
            return entity;
        }

        public  IQueryable<TEntity> GetAllQueryable()
        {
           return _context.Set<TEntity>();
        }

        public  IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity,bool>> whereExpression)
        {
            return _context.Set<TEntity>().Where(whereExpression);
        }

        public async Task<OutputEntity> GetAsync<OutputEntity>(Guid id, Expression<Func<TEntity, OutputEntity>> selectedExpression,Expression<Func<TEntity,bool>> predicateExpression)
        {
            return await _context.Set<TEntity>().Where(predicateExpression).Select(selectedExpression).FirstOrDefaultAsync();
        }
    }
}
