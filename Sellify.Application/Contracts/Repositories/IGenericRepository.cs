using Sellify.Domain.Contracts;
using System.Linq.Expressions;

namespace Sellify.Application.Contracts.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, ISoftDeletable, IEntityUpdatable
    {
        public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken= default);
        public TEntity Update(TEntity entity);
        public TEntity Delete(TEntity entity);

        public IQueryable<TEntity> GetAllQueryable();
        public IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>> whereExpression );

        public Task<OutputEntity> GetAsync<OutputEntity>(Guid id, Expression<Func<TEntity, OutputEntity>> selectedExpression, Expression<Func<TEntity, bool>> predicateExpression);
        public Task<bool> IsExist(Expression<Func<TEntity, bool>> predicateExpression);

    }
}
