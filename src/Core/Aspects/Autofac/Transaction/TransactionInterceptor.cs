using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Results;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionInterceptor : InterceptorBase<TransactionAttribute>
    {
        public override void Intercept(IInvocation invocation)
        {
            var message = "";
            var attribute = GetAttribute(invocation.MethodInvocationTarget, invocation.TargetType);

            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();

                    if (invocation.ReturnValue is Task returnValueTask)
                        returnValueTask.GetAwaiter().GetResult();
                    if (invocation.ReturnValue is Task task && task.Exception != null)
                        throw task.Exception;

                    if (invocation.ReturnValue.GetType() is IBaseResult && !(invocation.ReturnValue as IBaseResult).Success)
                        throw new System.Exception((invocation.ReturnValue as IBaseResult).Message);

                    transactionScope.Complete();
                }
                catch (System.Exception ex)
                {
                    transactionScope.Dispose();

                    message = ex.Message;

                    if (!string.IsNullOrWhiteSpace(ex.InnerException?.Message))
                        message += $" - {ex.InnerException?.Message ?? ""}";

                    if (message.Contains("FOREIGN KEY"))
                        message = "Kayıt hatası!...";

                    var result = (IBaseResult)Activator.CreateInstance(attribute.returnType, args: message);

                    if (attribute.async)
                        invocation.ReturnValue = Task.FromResult(result);
                    else
                        invocation.ReturnValue = result;
                }
            }

            if (!string.IsNullOrEmpty(message))
                new LoggerServiceBase(false).Error(message);
        }

        private TransactionAttribute GetAttribute(MethodInfo methodInfo, Type type)
        {
            var attribute = methodInfo.GetCustomAttribute<TransactionAttribute>(true);
            if (attribute is not null)
                return attribute;
            return type.GetTypeInfo().GetCustomAttribute<TransactionAttribute>(true);
        }
    }
}
