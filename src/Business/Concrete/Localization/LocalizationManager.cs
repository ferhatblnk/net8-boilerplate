using Business.Abstract;

namespace Business.Concrete
{
    public class LocalizationManager : BaseManager, ILocalizationService
    {
        [YearlyCache(_includeLocalization = true)]
        public string T(string key)
        {
            var language = _currentLanguage;

            return _localizedMapDal.Get(x => x.MapKey == key && x.LanguageId.Equals(language.Id))?.Value ?? key;
        }
        [CacheRemove("ILocalizationService.T")]
        public void TReset(string key) { }
    }
}
