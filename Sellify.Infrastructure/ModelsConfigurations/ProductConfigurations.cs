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
            builder.Property(p => p.RowVersion).IsConcurrencyToken();
            #endregion

            #region Relationships
            builder.HasMany(p => p.ProductImages)
                  .WithOne(p => p.Product).HasForeignKey(p => p.ProductId);

            //builder.HasOne(p => p.Thumbnail)
            //    .WithOne(pi=> pi.ProductUsesThumbnail)
            //    .HasForeignKey<Product>(p => p.ThumbnailId)
            //    .IsRequired(false);

            builder.HasOne(p => p.Seller)
                .WithMany(p => p.Products).HasForeignKey(p => p.SellerId);

            builder.HasMany(p=> p.Reviews)
                .WithOne(r=>r.Product).HasForeignKey(r=>r.ProductId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.OrderItems)
                .WithOne(oi=> oi.Product).HasForeignKey(oi => oi.ProductId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.UserWishlistProducts)
                .WithOne(uwp => uwp.Product).HasForeignKey(uwp => uwp.ProductId).OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}
