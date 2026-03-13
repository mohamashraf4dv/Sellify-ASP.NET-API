namespace Sellify.Infrastructure.ModelsConfigurations
{
    public class ReviewsConfigurations : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => new { r.ProductId, r.ApplicationUserId });
            builder.HasIndex(r => r.ProductId);
        }
    }
}
