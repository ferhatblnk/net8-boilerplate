using Core.Utilities.Results;
using Entities.Concrete.UserAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Abstract.UserAddress
{
    public interface IUserAddressService
    {
        IDataResult<TUserAddress> Create(TUserAddress userAddress);
        IDataResult<TUserAddress> Update(TUserAddress userAddress);
        IDataResult<TUserAddress> Delete(int id);
        IDataResult<TUserAddress> Get(int id);
        IDataResult<List<TUserAddress>> GetList();
    }
}