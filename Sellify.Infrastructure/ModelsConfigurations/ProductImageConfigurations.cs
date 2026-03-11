
namespace Sellify.Infrastructure.ModelsConfigurations
{
    public class ProductImageConfigurations : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(pi => pi.Id);
            builder.HasIndex(pi => pi.ProductId);

            builder.Property(pi => pi.Url).IsRequired();
            builder.Property(pi => pi.Description).IsRequired();

        }
    }
}
