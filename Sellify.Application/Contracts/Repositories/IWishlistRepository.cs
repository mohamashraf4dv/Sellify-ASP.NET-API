using Sellify.Domain.Enums;

namespace Sellify.Application.Contracts.Repositories
{
    public interface IWishlistRepository: IGenericRepositoryWithNoSoftDeleteAndUpdate<UserWishlistProduct>
    {
        public Task<WishlistStatus> UpdateWishlistStatus(UserWishlistProduct userWishlistProduct,CancellationToken cancellationToken);
        public IQueryable<UserWishlistProduct> GetAllWishlistProductsByUserId(string UserId);
    }
}
