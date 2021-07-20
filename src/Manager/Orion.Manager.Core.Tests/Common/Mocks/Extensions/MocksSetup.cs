using Microsoft.Extensions.DependencyInjection;

namespace Orion.Manager.Core.Tests.Common.Mocks.Extensions
{
    public static class MocksSetup
    {
        public static IServiceCollection AddMockProviders(this IServiceCollection services)
        {
            return services;
        }
    }
}