namespace Sellify.Infrastructure.ModelsConfigurations
{
    public class PaymentSessionsConfigurations : IEntityTypeConfiguration<PaymentSession>
    {
        public void Configure(EntityTypeBuilder<PaymentSession> builder)
        {
            builder.HasKey(ps => ps.SessionId);
        }
    }
}
