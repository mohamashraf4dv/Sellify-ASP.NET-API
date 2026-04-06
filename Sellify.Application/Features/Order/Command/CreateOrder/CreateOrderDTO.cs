namespace Sellify.Application.Features.Order.Command.CreateOrder
{
    public record CreateOrderDTO(Guid ProductId, decimal Price, long Quantity);
}
