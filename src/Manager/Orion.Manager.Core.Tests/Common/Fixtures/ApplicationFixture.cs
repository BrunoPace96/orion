using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Orion.Manager.Core.Tests.Common.Mocks;
using Orion.Manager.Core.Tests.Common.Mocks.Extensions;
using Orion.Manager.Infra.Data.Context;
using Orion.Manager.Infra.IoC;

namespace Orion.Manager.Core.Tests.Common.Fixtures
{
    public class ApplicationFixture
    {
        public ApplicationFixture()
        {
            var appSettings = AppSettingsMocker.Mock();
            var services = new ServiceCollection();
            
            services
                .AddApplicationDependencies(appSettings, true)
                .AddMockProviders();
            
            void ConfigureDatabase()
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                var serviceDescriptor = services.FirstOrDefault(e => 
                    e.ServiceType == typeof(ApplicationDbContext)
                );
                services.Remove(serviceDescriptor);
                
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTestDatabase");
                    options.UseInternalServiceProvider(serviceProvider);
                });
            }

            ConfigureDatabase();

            ServiceProvider = services.BuildServiceProvider();

            void CreateInMemoryDatabase()
            {
                var scope = ServiceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                dbContext.Database.EnsureCreated();
            }

            CreateInMemoryDatabase();
        }

        public ServiceProvider ServiceProvider { get; }
    }
}