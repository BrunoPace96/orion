using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Orion.DomainValidation.Domain.Behaviours;
using Orion.Manager.Core.Users.Write.CreateUser;

namespace Orion.Manager.Infra.IoC.Setup
{
    public static class MediatorSetup
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateUserHandler).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastBehavior<,>));
            return services;
        }
    }
}