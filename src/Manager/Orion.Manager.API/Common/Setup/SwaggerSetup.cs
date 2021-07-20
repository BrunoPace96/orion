using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Orion.Manager.API.Common.Setup
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(e =>
            {
                e.SwaggerDoc("v1", new OpenApiInfo { Title = "Orion.Manager.API", Version = "v1" });
                e.EnableAnnotations();
            });
            
            return services;
        }
    }
}