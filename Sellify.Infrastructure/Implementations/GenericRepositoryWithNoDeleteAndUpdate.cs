namespace Sellify.Infrastructure.Implementations
{
    public class GenericRepositoryWithNoDeleteAndUpdate<TEntity> : IGenericRepositoryWithNoDeleteAndUpdate<TEntity> where TEntity: class
    {
        private readonly SellifyMicrosoftSqlContext _context;

        public GenericRepositoryWithNoDeleteAndUpdate(SellifyMicrosoftSqlContext context)
        {
            this._context = context;
        }
        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            return entity;
        }
    }
}
