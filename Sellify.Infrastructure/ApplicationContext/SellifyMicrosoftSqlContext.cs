

namespace Sellify.Infrastructure.ApplicationContext
{
    internal class SellifyMicrosoftSqlContext:IdentityDbContext<ApplicationUser>
    {
        public SellifyMicrosoftSqlContext(DbContextOptions<SellifyMicrosoftSqlContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
