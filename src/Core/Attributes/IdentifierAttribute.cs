using System;

namespace Core.Attributes
{
    internal class IdentifierAttribute : Attribute
    {
        public Guid Identifier;

        public IdentifierAttribute(string Identifier)
        {
            this.Identifier = new Guid(Identifier);
        }
    }
}