using Microsoft.Extensions.DependencyInjection;
using Orion.Manager.API.Common.Filters;

namespace Orion.Manager.API.Common.Setup
{
    public static class EndpointsSetup
    {
        public static IServiceCollection AddEndpoints(this IServiceCollection services)
        {
            services.AddControllers(e =>
            {
                e.Filters.Add(new DomainValidationFilter());
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}