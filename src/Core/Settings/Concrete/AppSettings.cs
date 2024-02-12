using Core.Constants;
using Core.Settings.Abstract;

namespace Core.Settings.Concrete
{
    public class AppSettings : ISettings
    {
        public DataProvider DataProvider { get; set; }
        public string ConnectionString { get; set; }
        public StorageConfig StorageConfig { get; set; }
        public EMailSettings Email { get; set; }
    }
}
