using Sellify.Application.Global.Results;

namespace Sellify.Application.Features.Orders.Query.GetOrdersForAuthenticatedUser
{
    public sealed record GetOrdersForAuthenticatedUserQuery(string UserId) : IRequest<GenericResultDTO>;

}
