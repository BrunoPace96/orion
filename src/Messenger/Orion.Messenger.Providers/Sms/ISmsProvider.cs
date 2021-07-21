using System.Threading.Tasks;

namespace Orion.Messenger.Providers.Sms
{
    public interface ISmsProvider
    {
        Task SendAsync(string phone, string content);
    }
}