using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Orion.Manager.Core.Common.Providers.Sms;
using Orion.Manager.SharedKernel.Settings;

namespace Orion.Manager.Infra.Providers
{
    public class SmsProvider: SmsGrpcService.SmsGrpcServiceClient, ISmsProvider
    {
        private readonly AppSettings _appSettings;

        public SmsProvider(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        
        public override AsyncUnaryCall<Empty> SendSmsAsync(
            SmsRequest request, 
            CallOptions options
        )
        {
            var channel = GrpcChannel.ForAddress(_appSettings.Messenger.GrpcSmsUrl);
            var service = new SmsGrpcService.SmsGrpcServiceClient(channel);
            return service.SendSmsAsync(request);
        }

        public async Task SendAsync(SmsCommand command)
        {
            var request = new SmsRequest
            {
                Phone = command.Phone,
                Content = command.Content
            };

            await SendSmsAsync(request);
        }
    }
}