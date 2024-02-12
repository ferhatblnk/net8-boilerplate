namespace Business.Abstract
{
    public interface ILocalizationService
    {
        string T(string key);
        void TReset(string key);
    }
}
