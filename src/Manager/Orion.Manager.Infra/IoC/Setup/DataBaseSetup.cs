using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orion.Manager.Infra.Data.Context;
using Orion.Manager.SharedKernel.Settings;

namespace Orion.Manager.Infra.IoC.Setup
{
    public static class DataBaseSetup
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services, 
            AppSettings appSettings
        )
        {
            services.AddDbContext<ApplicationDbContext>(e =>
                e.UseSqlServer(appSettings.ConnectionString)
                    .ConfigureWarnings(x => x.Ignore(CoreEventId.SensitiveDataLoggingEnabledWarning))
                    .LogTo(
                        Console.WriteLine,
                        new[]
                        {
                            DbLoggerCategory.Database.Command.Name
                        },
                        LogLevel.Information,
                        DbContextLoggerOptions.DefaultWithLocalTime |
                        DbContextLoggerOptions.SingleLine)
                    .EnableSensitiveDataLogging()
            );

            return services;
        }
    }
}