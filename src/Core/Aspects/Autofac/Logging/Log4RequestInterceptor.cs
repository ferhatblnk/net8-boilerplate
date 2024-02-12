using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.Utilities.Interceptors;
using System.Collections.Generic;

namespace Core.Aspects.Autofac.Logging
{
    public class Log4RequestInterceptor : InterceptorBase<Log4RequestAttribute>
    {
        public Log4RequestInterceptor() { }

        protected override void OnBefore(IInvocation invocation, Log4RequestAttribute attribute)
        {
            attribute.Logger.Info(GetLogDetail(invocation));
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

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
