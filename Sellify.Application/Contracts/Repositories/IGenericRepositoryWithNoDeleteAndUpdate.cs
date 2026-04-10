namespace Sellify.Application.Contracts.Repositories
{
    public interface IGenericRepositoryWithNoDeleteAndUpdate<TEntity> where TEntity : class
    {
        public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    }
}
