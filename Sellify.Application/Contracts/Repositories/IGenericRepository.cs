using Sellify.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Sellify.Application.Contracts.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, ISoftDeletable, IEntityUpdatable
    {
        public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken= default);
        public TEntity Update(TEntity entity);
        public TEntity Delete(TEntity entity);

        public IQueryable<TEntity> GetAllQueryable();
        public IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>> whereExpression );
    }
}
