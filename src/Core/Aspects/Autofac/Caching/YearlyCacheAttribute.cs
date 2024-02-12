using Core.Utilities.Interceptors;

public class YearlyCacheAttribute(bool includeLocalization = false) : AttributeBase
{
    /// <summary>
    /// Set duration for caching by given seconds
    /// </summary>
    /// <value></value>
    public int _duration = 30000000;
    public bool _includeLocalization = includeLocalization;
}