using Core.CrossCuttingConcerns.Logging.Log4Db.Abstract;
using Core.Utilities.IoC;
using log4net;
using log4net.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    public class LoggerServiceBase
    {
        private ILog _log;

        public LoggerServiceBase(bool isFileLogger)
        {
            if (isFileLogger)
            {
                var Development = "";
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                if (environmentName == "Development")
                    Development = ".Development";

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(File.OpenRead($"log4net{Development}.config"));

                ILoggerRepository loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
                log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlDocument["log4net"]);

                _log = LogManager.GetLogger(loggerRepository.Name, "JsonFileLogger");
            }
            else
                _log = ServiceTool.ServiceProvider.GetService<ILog4DbService>();
        }

        public bool IsInfoEnabled => _log.IsInfoEnabled;
        public bool IsDebugEnabled => _log.IsDebugEnabled;
        public bool IsWarnEnabled => _log.IsWarnEnabled;
        public bool IsFatalEnabled => _log.IsFatalEnabled;
        public bool IsErrorEnabled => _log.IsErrorEnabled;

        public void Info(object logMessage)
        {
            if (IsInfoEnabled)
                _log.Info(logMessage);
        }

        public void Debug(object logMessage)
        {
            if (IsDebugEnabled)
                _log.Debug(logMessage);
        }

        public void Warn(object logMessage)
        {
            if (IsWarnEnabled)
                _log.Warn(logMessage);
        }

        public void Fatal(object logMessage)
        {
            if (IsFatalEnabled)
                _log.Fatal(logMessage);
        }

        public void Error(object logMessage)
        {
            if (IsErrorEnabled)
                _log.Error(logMessage);
        }
    }
}
