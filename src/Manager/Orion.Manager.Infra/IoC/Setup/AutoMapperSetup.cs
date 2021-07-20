using Microsoft.Extensions.DependencyInjection;

namespace Orion.Manager.Infra.IoC.Setup
{
    public static class AutoMapperSetup
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            // services.AddAutoMapper(typeof(UserMap).Assembly);
            return services;
        }
    }
}