
namespace Sellify.Infrastructure.Implementations.Repositories
{
    public class WishlistRepository : GenericRepositoryWithNoSoftDeleteAndUpdate<UserWishlistProduct>, IWishlistRepository
    {
        private readonly SellifyMicrosoftSqlContext _context;

        public WishlistRepository(SellifyMicrosoftSqlContext context) : base(context)
        {
            this._context = context;
        }

        public IQueryable<UserWishlistProduct> GetAllWishlistProductsByUserId(string UserId)
        {
           return _context.UserWishlistProducts.Where(uwp=> uwp.UserId== UserId);
        }

        public async Task<WishlistStatus> UpdateWishlistStatus(UserWishlistProduct userWishlistProduct, CancellationToken cancellationToken)
        {
            var existingUserWishListProduct = await _context.UserWishlistProducts
                .FirstOrDefaultAsync(uwp=> uwp.ProductId== userWishlistProduct.ProductId && uwp.UserId== userWishlistProduct.UserId);

            if (existingUserWishListProduct is null)
            {
                await _context.UserWishlistProducts.AddAsync(userWishlistProduct);
                return WishlistStatus.Added;
            }
            
            _context.UserWishlistProducts.Remove(existingUserWishListProduct);
            return WishlistStatus.Deleted;
        }
    }
}
