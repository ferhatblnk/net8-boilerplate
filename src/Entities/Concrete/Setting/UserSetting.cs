using System;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class TUserSetting : BaseEntity
    {
        public Guid UserId { get; set; }
        public string SettingKey { get; set; }
        public string Value { get; set; }

        public virtual TUser User { get; set; }
    }
}
