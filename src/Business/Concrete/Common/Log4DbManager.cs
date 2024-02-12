using Core.CrossCuttingConcerns.Logging.Log4Db.Abstract;
using Entities.Concrete;
using log4net.Core;
using Newtonsoft.Json;
using System;

namespace Business.Concrete
{
    public class Log4DbManager : BaseManager, ILog4DbService
    {
        public bool IsDebugEnabled => true;

        public bool IsInfoEnabled => true;

        public bool IsWarnEnabled => true;

        public bool IsErrorEnabled => true;

        public bool IsFatalEnabled => true;


        public ILogger Logger => default;


        private TSystemLog GetInput(object input, string audit)
        {
            var json = JsonConvert.SerializeObject(input, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return new TSystemLog
            {
                Detail = json
            };
        }

        private void SaveInput(TSystemLog input)
        {
            if (input != null)
            {
                try
                {
                    input.RowGuid = Guid.NewGuid();

                    _systemLogDal.Insert(input);
                }
                catch { }
            }
        }


        public void Debug(object message)
        {
            SaveInput(GetInput(message, "Debug"));
        }

        public void Debug(object message, Exception exception)
        {
        }

        public void DebugFormat(string format, params object[] args)
        {
        }

        public void DebugFormat(string format, object arg0)
        {
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
        }

        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
        }

        public void Error(object message)
        {
            SaveInput(GetInput(message, "Error"));
        }

        public void Error(object message, Exception exception)
        {
        }

        public void ErrorFormat(string format, params object[] args)
        {
        }

        public void ErrorFormat(string format, object arg0)
        {
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
        }

        public void Fatal(object message)
        {
            SaveInput(GetInput(message, "Fatal"));
        }

        public void Fatal(object message, Exception exception)
        {
        }

        public void FatalFormat(string format, params object[] args)
        {
        }

        public void FatalFormat(string format, object arg0)
        {
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
        }

        public void Info(object message)
        {
            SaveInput(GetInput(message, "Info"));
        }

        public void Info(object message, Exception exception)
        {
        }

        public void InfoFormat(string format, params object[] args)
        {
        }

        public void InfoFormat(string format, object arg0)
        {
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
        }

        public void Warn(object message)
        {
            SaveInput(GetInput(message, "Warn"));
        }

        public void Warn(object message, Exception exception)
        {
        }

        public void WarnFormat(string format, params object[] args)
        {
        }

        public void WarnFormat(string format, object arg0)
        {
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
        }
    }
}
