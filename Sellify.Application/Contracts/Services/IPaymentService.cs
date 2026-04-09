using Sellify.Application.Features.Orders.Command.CreateOrder;

namespace Sellify.Application.Contracts.Services
{
    public interface IPaymentService
    {
        public Task<string> GetPaymentIntentClientSecret(decimal amount, string currency = "usd");
        public Task<string> CreatePaymentSession(IReadOnlyList<Product> products, IReadOnlyDictionary<Guid, CreateOrderDTO> createOrderDto, string buyerId);


    }
}
