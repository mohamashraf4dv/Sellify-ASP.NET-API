namespace Sellify.Application.Features.Order.Command.CreateOrder
{
    public record CreateOrderRequest(IReadOnlyList<CreateOrderDTO> Order, IReadOnlyList<string> ProductsIds);

}
