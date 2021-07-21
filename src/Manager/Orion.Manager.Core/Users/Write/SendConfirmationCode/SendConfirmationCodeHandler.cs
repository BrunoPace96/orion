using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orion.Manager.Core.Common.Providers.Sms;
using Orion.Manager.Core.Common.ValueObjects;
using Orion.Manager.SharedKernel.Settings;

namespace Orion.Manager.Core.Users.Write.SendConfirmationCode
{
    public class SendConfirmationCodeHandler : 
        INotificationHandler<SendConfirmationCodeNotification>
    {
        private readonly AppSettings _appSettings;
        private readonly ISmsProvider _smsProvider;

        public SendConfirmationCodeHandler(
            AppSettings appSettings,
            ISmsProvider smsProvider
        )
        {
            _appSettings = appSettings;
            _smsProvider = smsProvider;
        }
        
        public async Task Handle(
            SendConfirmationCodeNotification notification, 
            CancellationToken cancellationToken
        )
        {
            var confirmationCode = new TwoFactorCode();
            var message = _appSettings.Messages.TwoFactorMessage;
            message = message.Replace("code-replace", confirmationCode.Value);
            
            var command = new SmsCommand
            {
                Phone = notification.Phone,
                Content = message
            };

            await _smsProvider.SendAsync(command);
        }
    }
}