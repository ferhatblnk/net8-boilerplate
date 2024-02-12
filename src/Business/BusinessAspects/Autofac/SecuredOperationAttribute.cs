using Core.Utilities.Interceptors;
using System;

public class SecuredOperationAttribute : AttributeBase
{
    public bool async;
    public string[] roles;
    public Type returnType;

    public SecuredOperationAttribute(string roles = "", Type returnType = null, bool async = false)
    {
        this.async = async;
        this.roles = roles.Split(',');
        this.returnType = returnType;
    }
}