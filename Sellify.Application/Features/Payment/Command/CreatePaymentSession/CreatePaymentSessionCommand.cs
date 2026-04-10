using Sellify.Application.Features.Payment.Command.CreatePaymentSession;

namespace Sellify.Application.Features.Payment.Command.CreatePaymentIntent
{
    public record CreatePaymentSessionCommand(
            IReadOnlyDictionary<Guid, CreatePaymentSessionUsingOrderDTO> OrderDTO,
            List<Guid> ProductsIds,
            string BuyerId) : IRequest<GenericResultDTO>;

}
