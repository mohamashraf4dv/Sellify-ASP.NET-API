namespace Sellify.Application.Features.Order.Command.CreateOrder
{
    public record CreateOrderCommand
        (
            IReadOnlyList<CreateOrderDTO> OrderDTO,
            IReadOnlyList<string> ProductsIds
        
        ) : IRequest<GenericResultDTO>;

}
