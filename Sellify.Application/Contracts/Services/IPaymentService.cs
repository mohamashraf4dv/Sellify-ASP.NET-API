using Sellify.Application.Features.Orders.Command.CreateOrder;
using Sellify.Application.Features.Payment.Command.CreatePaymentSession;

namespace Sellify.Application.Contracts.Services
{
    public interface IPaymentService
    {
        public Task<string> GetPaymentIntentClientSecret(decimal amount, string currency = "usd");
        public Task<string> CreatePaymentSession(IReadOnlyList<Product> products, IReadOnlyDictionary<Guid, CreatePaymentSessionUsingOrderDTO> createOrderDto, string buyerId);
        public Task<Dictionary<string, OrderSentFromStripeDTO>> GetOrderBySessionId(string sessionId);



    }
}
