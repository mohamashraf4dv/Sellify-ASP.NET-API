namespace Sellify.Domain.Entities
{
    //Saves Information about Stripe Session ->
    public class PaymentSession
    {
        public required string BuyerId { get; set; }
        public required string SessionId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
