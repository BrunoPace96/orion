namespace Orion.Messenger.FunctionApp.Functions.Sms
{
    public class SmsRequest
    {
        public string Phone { get; set; }
        public string Content { get; set; }
    }
}