namespace Sellify.Application.Features.Order.Command.CreateOrder
{
    public record CreateOrderRequest(IReadOnlyDictionary<Guid,CreateOrderDTO> Order, List<Guid> ProductsIds);

}
