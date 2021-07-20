using Microsoft.Extensions.DependencyInjection;

namespace Orion.Manager.Infra.IoC.Setup
{
    public static class HttpClientsSetup
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            return services;
        }
    }
}