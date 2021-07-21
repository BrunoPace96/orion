using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Orion.Messenger.Providers.Sms;

namespace Orion.Messenger.Services.Services
{
    public class SmsService: SmsGrpcService.SmsGrpcServiceBase
    {
        private readonly ISmsProvider _smsProvider;

        public SmsService(ISmsProvider smsProvider)
        {
            _smsProvider = smsProvider;
        }
        
        public async Task<Empty> SendSmsAsync(
            SmsRequest request, 
            ServerCallContext context
        )
        {
            await _smsProvider.SendAsync(request.Phone, request.Content);
            return new Empty();
        }
    }
}