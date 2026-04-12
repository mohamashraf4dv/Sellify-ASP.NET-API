using Sellify.Application.Contracts.Repositories;

namespace Sellify.Application.Features.Products.Command.UserWishlistsProduct.Query.GetUserWishlistedProducts
{
    public class GetUserWishlistedProductsQueryHandler : IRequestHandler<GetUserWishlistedProductsQuery, GenericResultDTO<IReadOnlyList<GetUserWishlistedProductsDTO>>>
    {
        private readonly IWishlistRepository _wishlistRepository;

        public GetUserWishlistedProductsQueryHandler(IWishlistRepository wishlistRepository)
        {
            this._wishlistRepository = wishlistRepository;
        }
        public async Task<GenericResultDTO<IReadOnlyList<GetUserWishlistedProductsDTO>>> Handle(GetUserWishlistedProductsQuery request, CancellationToken cancellationToken)
        {
           var wishlistProducts = _wishlistRepository.GetAllWishlistProductsByUserId(request.UserId).Select(wp=> new GetUserWishlistedProductsDTO(wp.ProductId,wp.Product.ThumbnailSource?? "",wp.Product.Name,wp.Product.Price,wp.Product.Stock,wp.WishedAt)).ToList().AsReadOnly();
            return new GenericResultDTO<IReadOnlyList<GetUserWishlistedProductsDTO>>(wishlistProducts,200);
        }
    }
}
