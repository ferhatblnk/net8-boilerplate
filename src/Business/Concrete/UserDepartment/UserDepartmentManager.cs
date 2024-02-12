using Business.Abstract.UserDepartment;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete.UserDepartment
{
    public class UserDepartmentManager : BaseManager, IUserDepartmentService
    {
        public IDataResult<TUserDepartment> Create(TUserDepartment userDepartment)
        {
            var result = _userDepartmentDal.Insert(userDepartment);
            if (result != null)
                return new SuccessDataResult<TUserDepartment>
                {
                    Data = result,
                };
            return new ErrorDataResult<TUserDepartment>();
        }

        public IDataResult<TUserDepartment> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<TUserDepartment> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<TUserDepartment>> GetList()
        {
            return new SuccessDataResult<List<TUserDepartment>> { Data = [.. _userDepartmentDal.GetList()] };
        }

        public IDataResult<TUserDepartment> Update(TUserDepartment TUserDepartment)
        {
            throw new NotImplementedException();
        }

    }
}