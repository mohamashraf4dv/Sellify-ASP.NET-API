using System.Linq.Expressions;

namespace Sellify.Application.Contracts.Repositories
{
    public interface IGenericRepositoryWithNoSoftDeleteAndUpdate<TEntity> where TEntity : class
    {
        public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        public Task Delete (TEntity entity);
        public Task<IReadOnlyList<TEntity>> GetAll();
        public Task<IReadOnlyList<TResultDTO>> GetAll<TResultDTO>(Expression<Func<TEntity, TResultDTO>> selectExpression);
        public Task<IReadOnlyList<TResultDTO>> GetAll<TResultDTO>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResultDTO>> selectExpression);
        public Task<IReadOnlyList<TResultDTO>> GetAll<TResultDTO, TGroupKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TGroupKey>> groupByExpression,
                                                                        Expression<Func<IGrouping<TGroupKey, TEntity>, TResultDTO>> selectExpression);
    }
}
