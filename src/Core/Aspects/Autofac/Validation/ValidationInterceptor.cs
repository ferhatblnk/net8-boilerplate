using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using Core.Utilities.Results;
using FluentValidation;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationInterceptor : InterceptorBase<ValidationAttribute>
    {
        public override void Intercept(IInvocation invocation)
        {
            var attribute = GetAttribute(invocation.MethodInvocationTarget, invocation.TargetType);

            try
            {
                var validator = (IValidator)Activator.CreateInstance(attribute.validatorType);
                var entityType = attribute.validatorType.BaseType.GetGenericArguments()[0];
                var entities = invocation.Arguments
                    .Where(t => t.GetType() == entityType)
                    .Select(x => new ValidationContext<object>(x))
                    .ToList();

                foreach (ValidationContext<object> entity in entities)
                {
                    ValidationTool<object>.Validate(validator, entity);
                }

                invocation.Proceed();
            }
            catch (System.Exception ex)
            {
                if (attribute.AllowThrow)
                    throw;

                var result = (IBaseResult)Activator.CreateInstance(attribute.returnType, args: $"{ex.Message} - {ex.InnerException?.Message ?? ""}");

                if (attribute.async)
                    invocation.ReturnValue = Task.FromResult(result);
                else
                    invocation.ReturnValue = result;
            }
        }

        private ValidationAttribute GetAttribute(MethodInfo methodInfo, Type type)
        {
            var attribute = methodInfo.GetCustomAttribute<ValidationAttribute>(true);
            if (attribute is not null)
                return attribute;
            return type.GetTypeInfo().GetCustomAttribute<ValidationAttribute>(true);
        }
    }
}
