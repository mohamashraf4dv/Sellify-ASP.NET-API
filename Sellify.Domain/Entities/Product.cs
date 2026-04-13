namespace Sellify.Domain.Entities
{
    public class Product:ISoftDeletable,IEntityUpdatable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal TotalSold { get; set; }
        public string? ThumbnailSource { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime? LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public Guid RowVersion { get; set; }
        public long Stock { get; set; } = 0;
        public bool IsInStock => Stock > 0;

        // -- Navigation Properties -- //

        public ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public ICollection<UserWishlistProduct> UserWishlistProducts { get; set; }= new HashSet<UserWishlistProduct>();
        public Seller Seller { get; set; }
        public string SellerId { get; set; }
        public Product BeginSellingTransaction(long quantity) 
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            if (quantity> Stock)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            Stock = Stock - quantity;
            TotalSold += quantity;

            return this;
        }
    }
}
