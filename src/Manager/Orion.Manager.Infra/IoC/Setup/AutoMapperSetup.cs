using Microsoft.Extensions.DependencyInjection;
using Orion.Manager.Core.Students.Read;

namespace Orion.Manager.Infra.IoC.Setup
{
    public static class AutoMapperSetup
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(StudentMap).Assembly);
            return services;
        }
    }
}