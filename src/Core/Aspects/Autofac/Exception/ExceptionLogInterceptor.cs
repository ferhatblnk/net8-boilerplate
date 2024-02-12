using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.Utilities.Interceptors;
using System.Collections.Generic;

namespace Core.Aspects.Autofac.Exception
{
    public class ExceptionLogInterceptor : InterceptorBase<ExceptionLogAttribute>
    {
        public ExceptionLogInterceptor() { }

        protected override void OnException(IInvocation invocation, System.Exception e, ExceptionLogAttribute attribute)
        {
            LogDetailWithException logDetailWithException = GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = e.Message;

            attribute.Logger.Error(logDetailWithException);
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            List<LogParameter> logParameters = [];

            try
            {
                for (int i = 0; i < invocation.Arguments.Length; i++)
                {
                    logParameters.Add(new LogParameter
                    {
                        Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                        Value = invocation.Arguments[i],
                        Type = invocation.Arguments[i].GetType().Name
                    });
                }
            }
            catch { }

            var logDetailWithException = new LogDetailWithException
            {
                TargetName = invocation.TargetType.Name,
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };

            return logDetailWithException;
        }
    }
}
