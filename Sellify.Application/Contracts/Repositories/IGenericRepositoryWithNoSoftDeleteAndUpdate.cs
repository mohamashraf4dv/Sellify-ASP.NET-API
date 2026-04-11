namespace Sellify.Application.Contracts.Repositories
{
    public interface IGenericRepositoryWithNoSoftDeleteAndUpdate<TEntity> where TEntity : class
    {
        public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        public Task Delete (TEntity entity);
    }
}
