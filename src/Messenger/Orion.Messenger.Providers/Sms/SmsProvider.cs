using System;
using System.Threading.Tasks;

namespace Orion.Messenger.Providers.Sms
{
    public class SmsProvider : ISmsProvider
    {
        public Task SendAsync(string phone, string content)
        {
            Console.WriteLine($"Sending sms to {phone}");
            Console.WriteLine(content);
            return Task.CompletedTask;
        }
    }
}