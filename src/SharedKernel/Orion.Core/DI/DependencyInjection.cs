using System;
using Microsoft.Extensions.DependencyInjection;
using Orion.Core.Services;

namespace Orion.Core.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(
            this IServiceCollection services,
            Type writeServiceImplementation
        )
        {
            services.AddTransient(typeof(IWriteService<>), writeServiceImplementation);
            return services;
        }
    }
}