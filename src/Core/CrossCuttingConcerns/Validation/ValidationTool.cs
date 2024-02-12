using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool<T>
    {
        public static void Validate(IValidator validator, ValidationContext<T> entity)
        {
            var result = validator.Validate(entity);

            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }
    }
}
