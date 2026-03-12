
namespace Sellify.Application.Contracts.RepoBehavior
{
    public interface IGetterEntity<TEntity> where TEntity : class
    {
        public Task<TEntity?> GetAsync(string id);
        public Task<IReadOnlyList<TEntity>> GetAllAsync();
    }
}
