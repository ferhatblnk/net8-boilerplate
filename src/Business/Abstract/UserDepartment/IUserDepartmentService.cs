using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.UserAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Abstract.UserDepartment
{
    public interface IUserDepartmentService
    {
        IDataResult<TUserDepartment> Create(TUserDepartment uerDepartment);
        IDataResult<TUserDepartment> Update(TUserDepartment uerDepartment);
        IDataResult<TUserDepartment> Delete(int id);
        IDataResult<TUserDepartment> Get(int id);
        IDataResult<List<TUserDepartment>> GetList();
    }
}