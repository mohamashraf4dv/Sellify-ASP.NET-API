namespace Sellify.Application.Features.Orders.Command.CreateOrder
{
    public record CreateOrderCommand
        (
            IReadOnlyDictionary<Guid,CreateOrderDTO> OrderDTO,
            List<Guid> ProductsIds,
            string BuyerId
        
        ) : IRequest<GenericResultDTO>;

}
