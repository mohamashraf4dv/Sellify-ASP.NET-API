namespace Sellify.Application.Features.Orders.Command.CreateOrder
{
    public record CreateOrderRequest(IReadOnlyDictionary<Guid,CreateOrderDTO> Order, List<Guid> ProductsIds);

}
