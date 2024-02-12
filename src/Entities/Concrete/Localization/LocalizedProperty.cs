using System;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class TLocalizedProperty : BaseEntity
    {
        public Guid LanguageId { get; set; }
        public string TableName { get; set; }
        public Guid TableId { get; set; }
        public string TableField { get; set; }
        public string Value { get; set; }

        public virtual TLanguage Language { get; set; }
    }
}
