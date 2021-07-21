using Microsoft.Extensions.DependencyInjection;
using Orion.Manager.Core.Common.Providers.Sms;

namespace Orion.Manager.Core.Tests.Common.Mocks.Extensions
{
    public static class MocksSetup
    {
        public static IServiceCollection AddMockProviders(this IServiceCollection services)
        {
            services.AddTransient<ISmsProvider, SmsMockProvider>();
            return services;
        }
    }
}