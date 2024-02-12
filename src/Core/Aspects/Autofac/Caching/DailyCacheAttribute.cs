using Core.Utilities.Interceptors;

public class DailyCacheAttribute(bool includeLocalization = false) : AttributeBase
{
    /// <summary>
    /// Set duration for caching by given seconds
    /// </summary>
    /// <value></value>
    public int _duration = 86400;
    public bool _includeLocalization = includeLocalization;
}