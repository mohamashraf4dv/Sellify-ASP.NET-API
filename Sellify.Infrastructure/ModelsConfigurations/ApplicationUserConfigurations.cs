

using Sellify.Domain.Enums;

namespace Sellify.Infrastructure.ModelsConfigurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            #region Property Configuration
            builder.Property(u => u.FirstName)
        .IsRequired()
        .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.DateOfBirth)
                .IsRequired();

            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.PhoneNumber)
                .IsRequired(false);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.SellerRoleRequestStatus)
                .HasConversion<string>();
            #endregion

            #region Relationships

            builder.HasOne(u => u.Seller)
                .WithOne().HasForeignKey<Seller>(s => s.Id);

            builder.HasMany(u => u.Reviews)
                .WithOne().HasForeignKey(r => r.ApplicationUserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(au => au.Token).WithOne().HasForeignKey<Token>(t => t.ApplicationUserId);

            builder.HasMany(u => u.Orders)
                .WithOne().HasForeignKey(o => o.BuyerId).OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
