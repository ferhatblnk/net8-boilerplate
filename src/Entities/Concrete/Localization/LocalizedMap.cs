using System;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class TLocalizedMap : BaseEntity
    {
        public Guid LanguageId { get; set; }
        public Guid? GroupCode { get; set; }
        public string MapKey { get; set; }
        public string Value { get; set; }

        public virtual TLanguage Language { get; set; }
    }
}
