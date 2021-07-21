namespace Orion.Manager.SharedKernel.Settings
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public Messenger Messenger { get; set; }
        public Messages Messages { get; set; }
    }
}