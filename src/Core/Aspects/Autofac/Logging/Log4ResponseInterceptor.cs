using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.Utilities.Interceptors;
using System.Collections.Generic;

namespace Core.Aspects.Autofac.Logging
{
    public class Log4ResponseInterceptor : InterceptorBase<Log4ResponseAttribute>
    {
        public Log4ResponseInterceptor() { }

        protected override void OnAfter(IInvocation invocation, Log4ResponseAttribute attribute)
        {
            attribute.Logger.Info(GetLogDetail(invocation));
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();

            logParameters.Add(new LogParameter
            {
                Type = "Response",
                Name = "Response",
                Value = invocation.ReturnValue
            });

            var logDetail = new LogDetail
            {
                TargetName = invocation.TargetType.Name,
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };

            return logDetail;
        }
    }
}
