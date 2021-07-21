using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Orion.Manager.Core.Users.Write.CreateUser;

namespace Orion.Manager.Infra.IoC.Setup
{
    public static class ValidationsSetup
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidation(e =>
            {
                e.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>();
            });
            return services;
        }
    }
}