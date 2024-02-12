using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Interceptors;

public class PerformanceAttribute : AttributeBase
{
    /// <summary>
    /// Set interval for checking by given milliseconds
    /// </summary>
    /// <value></value>
    public int Interval { get; set; } = 5000;
    public LoggerServiceBase Logger { get; set; } = new DatabaseLogger();

    public PerformanceAttribute() { }
    public PerformanceAttribute(int Interval)
    {
        this.Interval = Interval;
    }
}