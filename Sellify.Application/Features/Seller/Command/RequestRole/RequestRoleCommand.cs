using Sellify.Domain.Enums;

namespace Sellify.Application.Features.Seller.Command.RequestRole
{
    public record RequestRoleCommand(string RefreshToken,SellerRoleRequestStatus? SellerRoleRequestStatus = null) : IRequest<GenericResultDTO>;

}
