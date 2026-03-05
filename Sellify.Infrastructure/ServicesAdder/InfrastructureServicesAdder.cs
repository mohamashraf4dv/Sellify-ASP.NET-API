
namespace Sellify.Infrastructure.ServicesAdder
{
    public static class InfrastructureServicesAdder
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SellifyMicrosoftSqlContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<SellifyMicrosoftSqlContext>();
            return services;
        }
    }
}
