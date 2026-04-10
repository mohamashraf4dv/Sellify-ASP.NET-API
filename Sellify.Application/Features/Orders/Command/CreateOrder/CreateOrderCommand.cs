namespace Sellify.Application.Features.Orders.Command.CreateOrder
{
    public record CreateOrderCommand
        (
            string SessionId
        ) : IRequest<GenericResultDTO>;

}
