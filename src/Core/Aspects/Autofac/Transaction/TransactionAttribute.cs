using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using System;

public class TransactionAttribute : AttributeBase
{
    public bool async;
    public Type returnType;

    public TransactionAttribute(Type returnType, bool async = false)
    {
        if (returnType == null)
        {
            throw new System.Exception(AspectMessages.WrongReturnType);
        }

        this.async = async;
        this.returnType = returnType;
    }
}