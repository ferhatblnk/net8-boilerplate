using Business.Abstract.UserAddress;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Results;
using Entities.Concrete.UserAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete.UserAddress
{
    public class UserAddressManager : BaseManager, IUserAddressService
    {
    // [Validation(typeof(UserValidator),typeof(TUserAddress))]
        public IDataResult<TUserAddress> Create(TUserAddress TUserAddress)
        {
            var result = _userAddressDal.Insert(TUserAddress);
            if (result != null)
                return new SuccessDataResult<TUserAddress>
                {
                    Data = result,
                };
            return new ErrorDataResult<TUserAddress>();
        }

        public IDataResult<TUserAddress> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<TUserAddress> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<TUserAddress>> GetList()
        {
            return new SuccessDataResult<List<TUserAddress>> { Data = [.. _userAddressDal.GetList()] };
        }

        public IDataResult<TUserAddress> Update(TUserAddress TUserAddress)
        {
            throw new NotImplementedException();
        }

    }
}