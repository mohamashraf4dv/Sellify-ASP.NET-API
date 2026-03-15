namespace Sellify.Infrastructure.ModelsConfigurations
{
    public class TokenConfigurations : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasKey(t => t.ApplicationUserId);
        }
    }
}
