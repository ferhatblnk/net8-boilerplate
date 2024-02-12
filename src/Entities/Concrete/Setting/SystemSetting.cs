using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class TSystemSetting : BaseEntity
    {
        public string SettingKey { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
