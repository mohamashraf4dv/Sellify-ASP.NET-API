
namespace Sellify.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public long Stock { get; set; }
        public bool IsInStock => Stock > 0;

        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<ProductImage> ProductImages { get; set; }
        public ProductImage Thumbnail { get; set; }
    }
}
