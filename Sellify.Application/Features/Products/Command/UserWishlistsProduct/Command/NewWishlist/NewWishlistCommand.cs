using Sellify.Domain.Enums;

namespace Sellify.Application.Features.Products.Command.UserWishlistsProduct.Command.NewWishlist
{
    public record NewWishlistCommand
        (
        Guid ProductId,
        string UserId

        ) : IRequest<GenericResultDTO<WishlistStatus>>;

}
