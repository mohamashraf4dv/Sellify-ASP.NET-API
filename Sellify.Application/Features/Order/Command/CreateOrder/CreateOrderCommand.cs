namespace Sellify.Application.Features.Order.Command.CreateOrder
{
    public record CreateOrderCommand
        (
            IReadOnlyDictionary<Guid,CreateOrderDTO> OrderDTO,
            List<Guid> ProductsIds
        
        ) : IRequest<GenericResultDTO>;

}
