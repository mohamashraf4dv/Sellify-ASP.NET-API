namespace Sellify.Application.Contracts.Repositories
{
    public interface IReviewRepository
    {
        public Task<int> CreateIfNotExist(Review review);
    }
}
