using Core.Utilities.Results;
using Entities.Concrete.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Abstract.Role
{
    public interface IRoleService
    {
        IDataResult<TRole> Create(TRole role);
        IDataResult<TRole> Update(TRole role);
        IDataResult<TRole> Delete(int id);
        IDataResult<TRole> Get(int id);
        IDataResult<List<TRole>> GetList();
    }
}