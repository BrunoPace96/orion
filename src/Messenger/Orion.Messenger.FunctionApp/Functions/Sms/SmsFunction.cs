using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Orion.Messenger.Providers.Sms;

namespace Orion.Messenger.FunctionApp.Functions.Sms
{
    public class SmsFunction
    {
        private readonly ISmsProvider _smsProvider;

        public SmsFunction(ISmsProvider smsProvider)
        {
            _smsProvider = smsProvider;
        }
        
        [FunctionName("SendSms")]
        public async Task SendSms(
            [RabbitMQTrigger("orion-messenger-sms", ConnectionStringSetting = "BrokerRabbitMQ")] SmsRequest request,
            ILogger logger
        )
        {
             await _smsProvider.SendAsync(request.Phone, request.Content);
        }
    }
}