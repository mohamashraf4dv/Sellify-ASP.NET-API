
namespace Sellify.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public long Quantity { get; set; } = 0;

        public Product Product { get; set; }

    }
}
