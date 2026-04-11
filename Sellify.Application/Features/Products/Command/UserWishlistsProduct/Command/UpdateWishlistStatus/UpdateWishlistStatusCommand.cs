using Sellify.Domain.Enums;

namespace Sellify.Application.Features.Products.Command.UserWishlistsProduct.Command.UpdateWishlistStatus
{
    public record UpdateWishlistStatusCommand
        (
        Guid ProductId,
        string UserId

        ) : IRequest<GenericResultDTO<WishlistStatus>>;
}
