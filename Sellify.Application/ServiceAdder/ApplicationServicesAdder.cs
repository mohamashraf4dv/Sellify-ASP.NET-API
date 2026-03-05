


using System.Reflection;

namespace Sellify.Application.ServiceAdder
{
    public static class ApplicationServicesAdder
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(a=> Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
