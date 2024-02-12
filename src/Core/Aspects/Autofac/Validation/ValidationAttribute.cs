using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;
using System;

public class ValidationAttribute : AttributeBase
{
    public bool async;
    public Type validatorType;
    public Type returnType;

    public ValidationAttribute(Type validatorType, Type returnType, bool async = false)
    {
        if (validatorType == null || !typeof(IValidator).IsAssignableFrom(validatorType))
        {
            throw new System.Exception(AspectMessages.WrongValidationType);
        }
        else if (returnType == null)
        {
            throw new System.Exception(AspectMessages.WrongReturnType);
        }

        this.async = async;
        this.validatorType = validatorType;
        this.returnType = returnType;
    }
}