using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using System;

public class ExceptionLogAttribute : AttributeBase
{
    public LoggerServiceBase Logger { get; set; } = new DatabaseLogger();

    public ExceptionLogAttribute() { }
    public ExceptionLogAttribute(Type loggerService)
    {
        if (loggerService == null || loggerService.BaseType != typeof(LoggerServiceBase))
        {
            throw new System.Exception(AspectMessages.WrongLoggerType);
        }

        Logger = (LoggerServiceBase)Activator.CreateInstance(loggerService);
    }
}