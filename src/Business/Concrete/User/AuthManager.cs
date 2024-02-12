using Business.Abstract;
using Core.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Business.Utilities.Hash;

namespace Business.Concrete
{
    public class AuthManager : BaseManager, IAuthService
    {
        #region Methods

        public Task<IBaseResult> Login(LoginReqDto model)
        {
            var user = _userDal.Get(x => x.Email == model.Email);

            if (user == null)
                return Task.FromResult<IBaseResult>(new ErrorDataResult<LoginResDto>(Messages.UserNotFound));

           if(!CheckPwd(model.Password, user.Password)) return Task.FromResult<IBaseResult>(new ErrorDataResult<LoginResDto>(Messages.PasswordError));

            var token = _tokenHelper.CreateToken(user);

            if (string.IsNullOrWhiteSpace(token))
                return Task.FromResult<IBaseResult>(new ErrorDataResult<LoginResDto>(Messages.DataNotFound));

            var result = new LoginResDto
            {
                Id = user.Id,
                RowGuid = user.RowGuid,
                Token = token,
                Email = user.Email
            };

            return Task.FromResult<IBaseResult>(new SuccessDataResult<LoginResDto>(result, Messages.SuccessfulLogin));
        }

        public bool CheckPwd(string incoming, string current){
            var incomingPass =  PasswordHasher.HashPassword(incoming);
            if (incomingPass == current) return true;
            return false;
        }

        #endregion
    }
}
