using System.Threading.Tasks;
using Orion.Manager.Core.Common.Providers.Sms;

namespace Orion.Manager.Core.Tests.Common.Mocks
{
    public class SmsMockProvider: ISmsProvider
    {
        public Task SendAsync(SmsCommand command) => Task.CompletedTask;
    }
}