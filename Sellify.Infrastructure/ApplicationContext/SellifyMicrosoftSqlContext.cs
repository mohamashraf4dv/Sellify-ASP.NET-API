


namespace Sellify.Infrastructure.ApplicationContext
{
    public class SellifyMicrosoftSqlContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public SellifyMicrosoftSqlContext(DbContextOptions<SellifyMicrosoftSqlContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
