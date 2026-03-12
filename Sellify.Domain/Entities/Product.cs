
using Sellify.Domain.Contracts;

namespace Sellify.Domain.Entities
{
    public class Product:ISoftDeletable,IEntityUpdatable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public long Stock { get; set; } = 0;
        public bool IsInStock => Stock > 0;

        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();

        public ProductImage? Thumbnail { get; set; }
        public Guid? ThumbnailId { get; set; }
    }
}
