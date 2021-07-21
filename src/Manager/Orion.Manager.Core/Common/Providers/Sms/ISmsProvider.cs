using System.Threading.Tasks;

namespace Orion.Manager.Core.Common.Providers.Sms
{
    public interface ISmsProvider
    {
        public Task SendAsync(SmsCommand command);
    }
}