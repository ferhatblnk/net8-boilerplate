using Business.Abstract.Role;
using Core.Utilities.Results;
using Entities.Concrete.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete.Role
{
    public class UserRoleManager : BaseManager, IUserRoleService
    {
        public IDataResult<TUserRole> Create(TUserRole role)
        {
            var result = _userRoleDal.Insert(role);
            if (result != null)
                return new SuccessDataResult<TUserRole>
                {
                    Data = result,
                };
            return new ErrorDataResult<TUserRole>();
        }

        public IDataResult<TUserRole> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<TUserRole> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<TUserRole>> GetList()
        {
            return new SuccessDataResult<List<TUserRole>> { Data = [.. _userRoleDal.GetList()] };
        }

        public IDataResult<TUserRole> Update(TUserRole role)
        {
            throw new NotImplementedException();
        }
    }
}