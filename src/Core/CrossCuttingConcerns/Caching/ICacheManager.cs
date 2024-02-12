namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object data, int seconds);
        bool IsAdd(string key);
        void Remove(string key);
    }
}
