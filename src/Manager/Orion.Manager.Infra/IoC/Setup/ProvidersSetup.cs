using Microsoft.Extensions.DependencyInjection;
using Orion.Manager.Core.Common.Providers.Sms;
using Orion.Manager.Infra.Providers;

namespace Orion.Manager.Infra.IoC.Setup
{
    public static class ProvidersSetup
    {
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddTransient<ISmsProvider, SmsProvider>();
            return services;
        }
    }
}