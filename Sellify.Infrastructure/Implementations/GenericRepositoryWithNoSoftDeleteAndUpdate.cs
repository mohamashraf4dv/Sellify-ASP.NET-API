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
    }
}
