namespace Sellify.Infrastructure.ModelsConfigurations
{
    public class SellerConfigurations : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
