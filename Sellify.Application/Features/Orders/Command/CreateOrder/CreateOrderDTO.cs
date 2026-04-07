namespace Sellify.Application.Features.Orders.Command.CreateOrder
{
    //public record CreateOrderDTO(Guid ProductId, decimal Price, long Quantity);
    public record CreateOrderDTO( decimal Price, long Quantity);
}
