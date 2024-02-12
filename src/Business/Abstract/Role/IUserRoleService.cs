using Core.Utilities.Results;
using Entities.Concrete.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Abstract.Role
{
    public interface IUserRoleService
    {
        IDataResult<TUserRole> Create(TUserRole userRole);
        IDataResult<TUserRole> Update(TUserRole userRole);
        IDataResult<TUserRole> Delete(int id);
        IDataResult<TUserRole> Get(int id);
        IDataResult<List<TUserRole>> GetList();
    }
}