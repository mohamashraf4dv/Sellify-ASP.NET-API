namespace Sellify.Application.Contracts.Services
{
    public interface IOrderService
    {
        public Task<dynamic> GetSellerOrders(string orderId);
    }
}
