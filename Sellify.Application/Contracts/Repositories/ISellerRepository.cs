
namespace Sellify.Application.Contracts.Repositories
{
    public interface ISellerRepository
    {
        public Task CreateAsync(Seller seller);

    }
}
