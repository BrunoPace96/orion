using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Orion.Manager.Infra.IoC.Setup
{
    public static class ValidationsSetup
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidation(e =>
            {
                // e.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>();
            });
            return services;
        }
    }
}