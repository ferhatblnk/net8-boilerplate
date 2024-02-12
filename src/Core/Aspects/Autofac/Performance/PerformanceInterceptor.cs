using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System.Diagnostics;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceInterceptor : InterceptorBase<PerformanceAttribute>
    {
        private readonly Stopwatch _stopwatch;

        public PerformanceInterceptor()
        {
            _stopwatch = new Stopwatch();
        }

        protected override void OnBefore(IInvocation invocation, PerformanceAttribute attribute)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation, PerformanceAttribute attribute)
        {
            if (_stopwatch.Elapsed.TotalMilliseconds > attribute.Interval)
            {
                attribute.Logger.Warn($"{invocation.Method.Name} elapsed {_stopwatch.Elapsed.TotalSeconds} millisecond(s).");
            }

            _stopwatch.Reset();
            _stopwatch.Stop();
        }
    }
}
