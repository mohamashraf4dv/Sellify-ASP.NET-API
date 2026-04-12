

using Sellify.Infrastructure.AppSettingOptions;
using Sellify.Infrastructure.Implementations;

namespace Sellify.Infrastructure.ServicesAdder
{
    public static class InfrastructureServicesAdder
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<SellifyDapperContext>();
            services.AddDbContext<SellifyMicrosoftSqlContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentityCore<ApplicationUser>(o=> {
                o.Password.RequiredUniqueChars = 0;
                o.Password.RequiredLength = 6;
                o.Password.RequireUppercase = false;
                o.Password.RequireDigit = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireLowercase = false;
                                }).AddRoles<IdentityRole>().AddEntityFrameworkStores<SellifyMicrosoftSqlContext>();

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                    ClockSkew= TimeSpan.Zero
                };
            });

            #region Scoped Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IWishlistRepository, WishlistRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped(typeof(IGenericRepositoryWithNoSoftDeleteAndUpdate<>),typeof(GenericRepositoryWithNoSoftDeleteAndUpdate<>) );
            #endregion

            #region Scoped Services
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            services.Configure<URLS>(configuration.GetSection("URLS"));
            Stripe.StripeConfiguration.ApiKey = configuration.GetSection("Stripe").Get<StripeOptions>().SecretKey;

            


            services.AddScoped<UserHelper>();
            return services;
        }
    }
}
