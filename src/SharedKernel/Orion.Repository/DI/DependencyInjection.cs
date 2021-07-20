using System;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Orion.Repository.Repository;
using Orion.Repository.UnitOfWork.Factories;

namespace Orion.Repository.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            Type readOnlyRepositoryImplementation,
            Type repositoryImplementation,
            Type unitOfWorkScopeFactory
        )
        {
            services.AddTransient(typeof(IReadOnlyRepository<>), readOnlyRepositoryImplementation);
            services.AddTransient(typeof(IRepository<>), repositoryImplementation);
            services.AddScoped(typeof(IUnitOfWorkScopeFactory), unitOfWorkScopeFactory);
            services.AddTransient<ISpecificationEvaluator, SpecificationEvaluator>();
            return services;
        }
    }
}