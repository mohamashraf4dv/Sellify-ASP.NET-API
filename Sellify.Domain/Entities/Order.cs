namespace Sellify.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
        public string BuyerId { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; }= new HashSet<OrderItem>();
    }
}
