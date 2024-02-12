using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class TLanguage : BaseEntity
    {
        public string Name { get; set; }
        public string LanguageCode { get; set; }
        public string FlagUrl { get; set; }
    }
}
