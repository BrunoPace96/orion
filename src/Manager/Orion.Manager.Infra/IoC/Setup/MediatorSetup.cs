using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Orion.DomainValidation.Domain.Behaviours;
using Orion.Manager.Core.Students.Write.CreateStudent;

namespace Orion.Manager.Infra.IoC.Setup
{
    public static class MediatorSetup
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateStudentHandler).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastBehavior<,>));
            return services;
        }
    }
}