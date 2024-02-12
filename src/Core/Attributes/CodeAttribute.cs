using System;

namespace Core.Attributes
{
    internal class CodeAttribute : Attribute
    {
        public string Code;

        public CodeAttribute(string Code)
        {
            this.Code = Code;
        }
    }
}