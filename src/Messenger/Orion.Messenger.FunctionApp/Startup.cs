using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Orion.Messenger.FunctionApp;
using Orion.Messenger.Providers.Sms;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Orion.Messenger.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<ISmsProvider, SmsProvider>();
        }
    }
}