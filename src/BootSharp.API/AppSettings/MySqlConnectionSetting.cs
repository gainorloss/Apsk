using Infrastructure.Annotations;

namespace BootSharp.API.AppSettings
{
    [PropertySource()]
    public class MySqlConnectionSetting
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Uid { get; set; }
        public string Password { get; set; }
    }
}
