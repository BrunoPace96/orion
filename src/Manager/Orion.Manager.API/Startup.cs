using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orion.AspNet.Middlewares;
using Orion.Manager.API.Common.Setup;
using Orion.Manager.Infra.IoC;
using Orion.Manager.SharedKernel.Settings;

namespace Orion.Manager.API
{
    public class Startup
    {
        private AppSettings AppSettings { get; }

        public Startup(IConfiguration configuration)
        {
            AppSettings = configuration.Get<AppSettings>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddEndpoints()
                .AddSwagger()
                .AddApplicationDependencies(AppSettings);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orion.Manager.API v1"));
            }

            Action<string> logger = Console.WriteLine;
            app.UseMiddleware<LoggingMiddleware>(logger);
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}