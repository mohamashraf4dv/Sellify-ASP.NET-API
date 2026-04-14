namespace Sellify.Infrastructure.Implementations
{
    public class GenericRepositoryWithNoSoftDeleteAndUpdate<TEntity> : IGenericRepositoryWithNoSoftDeleteAndUpdate<TEntity> where TEntity: class
    {
        private readonly SellifyMicrosoftSqlContext _context;

        public GenericRepositoryWithNoSoftDeleteAndUpdate(SellifyMicrosoftSqlContext context)
        {
            this._context = context;
        }
        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            return entity;
        }
        public Task Delete(TEntity entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }
        public async Task<IReadOnlyList<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        public async Task<IReadOnlyList<TResultDTO>> GetAll<TResultDTO>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResultDTO>> selectExpression)
        {
            return await _context.Set<TEntity>().Where(predicate).Select(selectExpression).ToListAsync();
        }
        public async Task<IReadOnlyList<TResultDTO>> GetAll<TResultDTO>(Expression<Func<TEntity, TResultDTO>> selectExpression)
        {
            return await _context.Set<TEntity>().Select(selectExpression).ToListAsync();
        }
        public async Task<IReadOnlyList<TResultDTO>> GetAll<TResultDTO, TGroupKey>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TGroupKey>> groupByExpression,
            Expression<Func<IGrouping<TGroupKey, TEntity>, TResultDTO>> selectExpression)
        {
            return await _context.Set<TEntity>().Where(predicate).GroupBy(groupByExpression).Select(selectExpression).ToListAsync();
        }
    }
}
