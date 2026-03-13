
using Sellify.Domain.Entities;

namespace Sellify.Infrastructure.ModelsConfigurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Name);

            #region Properties configurations
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Stock).IsRequired();
            builder.Property(p => p.RowVersion).IsRowVersion();
            #endregion

            #region Relationships
            builder.HasMany(p => p.ProductImages)
                  .WithOne(p => p.Product).HasForeignKey(p => p.ProductId);

            builder.HasOne(p => p.Thumbnail)
                .WithOne()
                .HasForeignKey<Product>(p => p.ThumbnailId);

            builder.HasOne(p => p.Seller)
                .WithMany(p => p.Products).HasForeignKey(p => p.SellerId);
            #endregion
        }
    }
}
