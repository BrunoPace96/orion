using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Orion.DomainValidation.DI;
using Orion.DomainValidation.Domain.Behaviours;
using Orion.Manager.Infra.Data.Repositories;
using Orion.Manager.Infra.Data.UnitOfWork;
using Orion.Manager.Infra.IoC.Setup;
using Orion.Manager.SharedKernel.Settings;
using Orion.Repository.DI;

namespace Orion.Manager.Infra.IoC
{
    public static class Injector
    {
        public static IServiceCollection AddApplicationDependencies(
            this IServiceCollection services,
            AppSettings appSettings,
            bool isTestEnv = false
        )
        {
            services
                .AddRepositories(typeof(ReadOnlyRepository<>), typeof(Repository<>), typeof(UnitOfWorkScopeFactory))
                .AddDomainValidation()
                .AddHttpClients()
                .AddMapper()
                .AddMediator()
                .AddSingleton(appSettings)
                .AddProviders();

            if (!isTestEnv)
            {
                services
                    .AddDatabase(appSettings)
                    .AddProviders();
            }
                
            return services;
        } 
    }
}