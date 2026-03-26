
namespace Sellify.Infrastructure.ModelsConfigurations
{
    public class IdentityRoleConfigurations : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData
                (
                new IdentityRole() { Id= "0F9E7582-B7A3-4A6F-B1CC-79F2F350C2FF", Name= "Seller",NormalizedName="SELLER",ConcurrencyStamp= "0F9E7582-B7A3-4A6F-B1CC-79F2F350C2FF" },
                new IdentityRole() { Id= "C8CA233C-8C42-433D-A3ED-D5A8A2E7FC77", Name= "Admin", NormalizedName= "ADMIN", ConcurrencyStamp= "C8CA233C-8C42-433D-A3ED-D5A8A2E7FC77" }
                );
        }
    }
}
