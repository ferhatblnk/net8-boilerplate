using Core.Utilities.Interceptors;

public class MonthlyCacheAttribute(bool includeLocalization = false) : AttributeBase
{
    /// <summary>
    /// Set duration for caching by given seconds
    /// </summary>
    /// <value></value>
    public int _duration = 2500000;
    public bool _includeLocalization = includeLocalization;
}