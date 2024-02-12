using Castle.DynamicProxy;
using Core.Constants;
using Core.CrossCuttingConcerns.Caching;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheInterceptor : InterceptorBase<CacheAttribute>
    {
        private readonly ICacheManager _cacheManager;
        private readonly IHttpContextAccessor _httpContext;

        public CacheInterceptor()
        {
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
            _httpContext = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        private string GetRequestLanguage() => _httpContext.HttpContext.Request.Headers["lng"].FirstOrDefault() ?? Languages.Turkish.Code();

        public override void Intercept(IInvocation invocation)
        {
            var fullName = invocation.Method.ReflectedType.FullName;
            var fullNameParts = fullName.Split('.');
            fullName = fullNameParts[fullNameParts.Length - 1];

            var attribute = GetAttribute(invocation.MethodInvocationTarget, invocation.TargetType);

            var methodName = string.Format($"{fullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var localizationPrefix = attribute._includeLocalization ? $"{GetRequestLanguage()}-" : "";
            var key = $"{localizationPrefix}{methodName}({JsonSerializer.Serialize(arguments)})";

            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }

            invocation.Proceed();

            var globalKey = $"all-{methodName}({JsonSerializer.Serialize(arguments)})";
            var requests = new List<string>();

            if (_cacheManager.IsAdd(globalKey))
                requests = (_cacheManager.Get(globalKey) as List<string>) ?? [];

            requests.Add(key);

            _cacheManager.Remove(globalKey);
            _cacheManager.Add(globalKey, requests, attribute._duration);

            _cacheManager.Add(key, invocation.ReturnValue, attribute._duration);
        }

        private CacheAttribute GetAttribute(MethodInfo methodInfo, Type type)
        {
            var attribute = methodInfo.GetCustomAttribute<CacheAttribute>(true);
            if (attribute is not null)
                return attribute;
            return type.GetTypeInfo().GetCustomAttribute<CacheAttribute>(true);
        }
    }
}
