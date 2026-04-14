namespace Sellify.Infrastructure.ApplicationContext
{
    public class SellifyMicrosoftSqlContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<UserWishlistProduct> UserWishlistProducts { get; set; }
        public DbSet<PaymentSession> PaymentSessions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public SellifyMicrosoftSqlContext(DbContextOptions<SellifyMicrosoftSqlContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
