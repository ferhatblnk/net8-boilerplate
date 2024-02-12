using Business.Abstract;
using Core.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Business.Utilities.Security.Jwt;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Core.Extensions;
using System.Security.Cryptography;
using System.Text;
using Business.Utilities.Hash;

namespace Business.Concrete
{
    public class UserManager(IConfiguration configuration) : BaseManager, IUserService
    {
        private readonly TokenOptions tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        [YearlyCache]
        public virtual Task<IDataResult<UserResDto>> Get(Guid userId)
        {
            var result = _userDal.Get(userId);

            if (result != null)
                return Task.FromResult<IDataResult<UserResDto>>(new SuccessDataResult<UserResDto>(result.ToDto<UserResDto>()));

            return Task.FromResult<IDataResult<UserResDto>>(new ErrorDataResult<UserResDto>(LS.T(Messages.DataNotFound)));
        }
        [CacheRemove("IUserService.Get")]
        public void GetReset(Guid userId) { }
        [YearlyCache]
        public IDataResult<IList<UserResDto>> GetAll()
        {
            var result = _userDal
                            .GetList(x => !x.Deleted)
                            .Select(x => new UserResDto
                            {
                                Id = x.Id,
                                RowGuid = x.RowGuid,
                                Email = x.Email,
                                FirstName = x.FirstName,
                                LastName = x.LastName
                            })
                            .ToList();

            // return IDataResult<IList<UserResDto>>(new SuccessDataResult<IList<UserResDto>>(result));
            return new SuccessDataResult<List<UserResDto>>(result);
        }
        [CacheRemove("IUserService.GetAll")]
        public void GetAllReset() { }
        public virtual IDataResult<TUser> GetUserByMail(string email)
        {
            var result = _userDal.Get(x => x.Email == email && !x.Deleted);

            if (result != null)
                return new SuccessDataResult<TUser>(result);

            return new ErrorDataResult<TUser>(LS.T(Messages.DataNotFound));
        }
        public virtual IDataResult<TUser> GetUserByToken(string token)
        {
            var tokenHelper = ServiceTool.ServiceProvider.GetService<ITokenHelper>();
            var idStr = tokenHelper.GetClaim(token, ClaimNames.Id);

            _ = Guid.TryParse(idStr, out Guid id);
            var result = _userDal.Get(id);

            if (result != null && result.Token == token && _tokenHelper.Verify(result))
                return new SuccessDataResult<TUser>(result);

            return new ErrorDataResult<TUser>(LS.T(Messages.DataNotFound));
        }
        public virtual IBaseResult UpdateToken(Guid userId, string token)
        {
            var user = _userDal.Get(userId);

            return UpdateToken(user, token);
        }
        public virtual IBaseResult UpdateToken(TUser user, string token)
        {
            if (user == null)
                return new ErrorResult();

            user.Token = token;
            user.TokenExpiredAt = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);

            _userDal.Update(user);

            return new SuccessResult();
        }
        public IBaseResult RefreshToken(TUser user)
        {
            if (user == null)
                return new ErrorResult();

            if (!string.IsNullOrWhiteSpace(user.Token))
            {
                user.TokenExpiredAt = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);

                _userDal.Update(user);
            }

            return new SuccessResult();
        }
        public virtual IDataResult<string> CreateUserAccessToken(TUser user)
        {
            var token = _tokenHelper.CreateToken(user);

            UpdateToken(user, token);

            return new SuccessDataResult<string>(token, LS.T(Messages.AccessTokenCreated));
        }
        public void LockPassword(TUser user)
        {
            _userDal.Update(user);
        }
        public void UnlockPassword(TUser user)
        {
            _userDal.Update(user);
        }
        public void ClearToken(TUser user)
        {
            user.Token = "";
            user.TokenExpiredAt = null;
            _userDal.Update(user);
        }
        public IDataResult<UserResDto> Create(UserDto user)
        {
            var model = PropertyMapper.ToEntity<TUser>(user);
            model.Password = PasswordHasher.HashPassword(model.Password);
            var result = _userDal.Insert(model);
            if (result != null)
                return new SuccessDataResult<UserResDto>
                {
                    Data = result.ToDto<UserResDto>(),
                };
            return new ErrorDataResult<UserResDto>();
        }
    }
}
