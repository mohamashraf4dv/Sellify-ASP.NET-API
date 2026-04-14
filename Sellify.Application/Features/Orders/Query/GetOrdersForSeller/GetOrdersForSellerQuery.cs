namespace Sellify.Application.Features.Orders.Query.GetOrdersForSeller
{
    public sealed record GetOrdersForSellerQuery(string UserId) : IRequest<GenericResultDTO>;

}
