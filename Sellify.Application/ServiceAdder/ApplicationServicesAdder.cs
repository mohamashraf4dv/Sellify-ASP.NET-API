using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Sellify.Application.Behavior;
using Sellify.Application.Global;
using Sellify.Application.Mapper.AutoMapperProfiles;
using System.Reflection;

namespace Sellify.Application.ServiceAdder
{
    public static class ApplicationServicesAdder
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(o=>
            {
                o.RegisterServicesFromAssemblyContaining(typeof(ApplicationServicesAdder));
            });
            services.AddValidatorsFromAssemblyContaining(typeof(ApplicationServicesAdder));
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehaviorPipeline<,>));
            services.AddAutoMapper(cnf=> cnf.AddProfile(new MappingProfile()));
            return services;
        }
    }
}
