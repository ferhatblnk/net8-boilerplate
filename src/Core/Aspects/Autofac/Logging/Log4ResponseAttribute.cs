using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using System;

public class Log4ResponseAttribute : AttributeBase
{
    public LoggerServiceBase Logger { get; set; } = new DatabaseLogger();

    public Log4ResponseAttribute() { }
    public Log4ResponseAttribute(Type loggerService)
    {
        if (loggerService == null || loggerService.BaseType != typeof(LoggerServiceBase))
        {
            throw new System.Exception(AspectMessages.WrongLoggerType);
        }

        Logger = (LoggerServiceBase)Activator.CreateInstance(loggerService);
    }
}