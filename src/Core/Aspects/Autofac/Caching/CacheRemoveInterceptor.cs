using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveInterceptor : InterceptorBase<CacheRemoveAttribute>
    {
        private ICacheManager _cacheManager;

        public CacheRemoveInterceptor()
        {
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnBefore(IInvocation invocation, CacheRemoveAttribute attribute)
        {
            var methodName = attribute._method;
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({JsonSerializer.Serialize(arguments)})";
            var globalKey = $"all-{methodName}({JsonSerializer.Serialize(arguments)})";

            var requests = new List<string>();

            if (_cacheManager.IsAdd(globalKey))
                requests = (_cacheManager.Get(globalKey) as List<string>) ?? [];

            foreach (var request in requests)
            {
                _cacheManager.Remove(request);
            }

            if (attribute._all)
            {
                var allKey = $"all-{methodName}";

                var allRequests = new List<string>();

                if (_cacheManager.IsAdd(allKey))
                    allRequests = (_cacheManager.Get(allKey) as List<string>) ?? [];

                foreach (var request in allRequests)
                {
                    _cacheManager.Remove(request);
                }
            }

            _cacheManager.Remove(key);
        }
    }
}
