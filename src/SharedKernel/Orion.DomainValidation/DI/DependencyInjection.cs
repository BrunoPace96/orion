using Microsoft.Extensions.DependencyInjection;
using Orion.DomainValidation.Domain;

namespace Orion.DomainValidation.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainValidation(this IServiceCollection services)
        {
            services.AddScoped<IDomainValidationProvider, DomainValidationProvider>();
            return services;
        }
    }
}