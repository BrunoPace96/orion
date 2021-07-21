namespace Orion.Manager.Core.Common.Providers.Sms
{
    public record SmsCommand
    {
        public string Content { get; set; }
        public string Phone { get; set; }
    }
}