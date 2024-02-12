using Business.Abstract.Role;
using Core.Utilities.Results;
using Entities.Concrete.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete.Role
{
    public class RoleManager : BaseManager, IRoleService
    {
        public IDataResult<TRole> Create(TRole role)
        {
            var result = _roleDal.Insert(role);
            if (result != null)
                return new SuccessDataResult<TRole>
                {
                    Data = result,
                };
            return new ErrorDataResult<TRole>();
        }

        public IDataResult<TRole> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<TRole> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<TRole>> GetList()
        {
            return new SuccessDataResult<List<TRole>> { Data = [.. _roleDal.GetList()] };
        }

        public IDataResult<TRole> Update(TRole role)
        {
            throw new NotImplementedException();
        }

    }
}