using MediatR;

namespace Orion.Manager.Core.Users.Write.SendConfirmationCode
{
    public record SendConfirmationCodeNotification(string Phone) : INotification;
}