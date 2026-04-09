using Sellify.Application.Features.Orders.Command.CreateOrder;

namespace Sellify.Application.Features.Payment.Command.CreatePaymentIntent
{
    public record CreatePaymentSessionCommand(
            IReadOnlyDictionary<Guid, CreateOrderDTO> OrderDTO,
            List<Guid> ProductsIds,
            string BuyerId) : IRequest<GenericResultDTO>;

}
