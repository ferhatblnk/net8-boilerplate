using Business.Abstract;
using Business.Utilities.Security.Jwt;
using Castle.DynamicProxy;
using Core.Constants;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperationInterceptor : InterceptorBase<SecuredOperationAttribute>
    {
        public override void Intercept(IInvocation invocation)
        {
            var attribute = GetAttribute(invocation.MethodInvocationTarget, invocation.TargetType);

            try
            {
                var _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault() ?? "";

                var path = _httpContextAccessor.HttpContext.Request.Path;
                var roles = attribute.roles.Where(x => !string.IsNullOrEmpty(x)).ToList();
                roles.Add(path);

                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    var token = authHeader.Replace("Bearer ", "");
                    var _tokenHelper = ServiceTool.ServiceProvider.GetService<ITokenHelper>();

                    if (_tokenHelper.Validate(token))
                    {
                        var userId = _tokenHelper.GetClaim(token, ClaimNames.Id);
                        var userGuid = new Guid(userId);

                        var _userDal = ServiceTool.ServiceProvider.GetService<IRepository<TUser>>();

                        var roleExist = _userDal.GetList(user => user.RowGuid == userGuid).Count() > 0;

                        if (roleExist)
                        {
                            invocation.Proceed();

                            return;
                        }
                    }
                }
            }
            catch { }

            var LS = ServiceTool.ServiceProvider.GetService<ILocalizationService>();

            var result = (IBaseResult)Activator.CreateInstance(attribute.returnType, args: LS.T(Messages.AuthorizationDenied));

            if (attribute.async)
                invocation.ReturnValue = Task.FromResult(result);
            else
                invocation.ReturnValue = result;
        }

        private SecuredOperationAttribute GetAttribute(MethodInfo methodInfo, Type type)
        {
            var attribute = methodInfo.GetCustomAttribute<SecuredOperationAttribute>(true);
            if (attribute is not null)
                return attribute;
            return type.GetTypeInfo().GetCustomAttribute<SecuredOperationAttribute>(true);
        }
    }

}
