using Core.Utilities.Interceptors;

public class CacheRemoveAttribute(string method, bool all = false) : AttributeBase
{
    public string _method = method;
    public bool _all = all;
}