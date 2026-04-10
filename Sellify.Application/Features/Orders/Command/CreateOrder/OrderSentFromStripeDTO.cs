namespace Sellify.Application.Features.Orders.Command.CreateOrder
{
    public record OrderSentFromStripeDTO(string SellerId, string BuyerId, string ProductId, decimal? Price, long? Quantity, decimal TotalAmount);

}
