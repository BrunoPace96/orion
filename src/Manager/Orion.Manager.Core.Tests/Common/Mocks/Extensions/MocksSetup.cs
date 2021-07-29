using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orion.Manager.Core.Common.Providers.Sms;

namespace Orion.Manager.Core.Tests.Common.Mocks.Extensions
{
    public static class MocksSetup
    {
        public static IServiceCollection AddMockProviders(this IServiceCollection services)
        {
            services.AddTransient<ISmsProvider, SmsMockProvider>();
            services.AddTransient(typeof(ILogger<>), typeof(LoggerMock<>));
            return services;
        }
    }
}