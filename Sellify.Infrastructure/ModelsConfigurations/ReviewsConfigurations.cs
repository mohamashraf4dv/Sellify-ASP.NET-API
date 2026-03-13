namespace Sellify.Infrastructure.ModelsConfigurations
{
    public class ReviewsConfigurations : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasIndex(r => r.ProductId);
            
        }
    }
}
