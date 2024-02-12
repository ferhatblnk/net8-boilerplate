using Core.Utilities.Interceptors;

public class CacheAttribute(bool includeLocalization = false, int duration = 60) : AttributeBase
{
    /// <summary>
    /// Set duration for caching by given seconds
    /// </summary>
    /// <value></value>
    public int _duration = duration;
    public bool _includeLocalization = includeLocalization;
}