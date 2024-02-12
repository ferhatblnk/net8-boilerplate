using Entities.Concrete;
using System.Collections.Generic;
using Core.Utilities.Results;
using System;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserService
    {
        IBaseResult UpdateToken(Guid userId, string token);
        IBaseResult UpdateToken(TUser user, string token);
        IBaseResult RefreshToken(TUser user);
        IDataResult<UserResDto> Create(UserDto department);

        Task<IDataResult<UserResDto>> Get(Guid userId);
        void GetReset(Guid id);
        IDataResult<IList<UserResDto>> GetAll();
        void GetAllReset();

        IDataResult<TUser> GetUserByMail(string email);
        IDataResult<TUser> GetUserByToken(string token);

        IDataResult<string> CreateUserAccessToken(TUser user);

        void LockPassword(TUser user);
        void UnlockPassword(TUser user);
        void ClearToken(TUser user);
    }
}
