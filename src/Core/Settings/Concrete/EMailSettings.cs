using Core.Constants;
using Core.Settings.Abstract;

namespace Core.Settings.Concrete
{
    public class EMailSettings : ISettings
    {
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public string MailServerAddress { get; set; }
        public int MailServerPort { get; set; }
        public string UserId { get; set; }
        public string UserPassword { get; set; }
    }
}
