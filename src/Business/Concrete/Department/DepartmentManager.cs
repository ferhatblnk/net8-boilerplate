using Business.Abstract.Department;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete.Department
{
    public class DepartmentManager : BaseManager, IDepartmentService
    {
        public IDataResult<TDepartment> Create(TDepartment department)
        {
            var result = _departmentDal.Insert(department);
            if (result != null)
                return new SuccessDataResult<TDepartment>
                {
                    Data = result,
                };
            return new ErrorDataResult<TDepartment>();
        }

        public IDataResult<TDepartment> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<TDepartment> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<TDepartment>> GetList()
        {
            return new SuccessDataResult<List<TDepartment>> { Data = [.. _departmentDal.GetList()] };
        }

        public IDataResult<TDepartment> Update(TDepartment department)
        {
            throw new NotImplementedException();
        }

    }
}