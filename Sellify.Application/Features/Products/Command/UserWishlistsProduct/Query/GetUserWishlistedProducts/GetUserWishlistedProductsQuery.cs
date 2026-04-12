namespace Sellify.Application.Features.Products.Command.UserWishlistsProduct.Query.GetUserWishlistedProducts
{
    public record GetUserWishlistedProductsQuery(string UserId) : IRequest<GenericResultDTO<IReadOnlyList<GetUserWishlistedProductsDTO>>>;

}
