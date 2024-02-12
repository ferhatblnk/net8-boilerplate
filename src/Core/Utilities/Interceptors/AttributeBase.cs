using System;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class AttributeBase : Attribute
    {
        public int Priority { get; set; } = 0;
        public bool AllowThrow { get; set; } = false;
    }
}
