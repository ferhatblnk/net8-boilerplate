using Business.Abstract;
using Core.Utilities.IoC;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<TUser>
    {
        private IRepository<TUser> _userDal;

        public UserValidator()
        {
            _userDal = ServiceTool.ServiceProvider.GetService<IRepository<TUser>>();

            var LS = ServiceTool.ServiceProvider.GetService<ILocalizationService>();

            RuleFor(p => p).Must(NameCheck).WithMessage(LS.T("name_is_empty"));
        }

        private bool NameCheck(TUser arg)
        {
            return !string.IsNullOrWhiteSpace(arg.FirstName);
        }
    }
}
