using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Abstract.Department
{
    public interface IDepartmentService
    {
        IDataResult<TDepartment> Create(TDepartment department);
        IDataResult<TDepartment> Update(TDepartment department);
        IDataResult<TDepartment> Delete(int id);
        IDataResult<TDepartment> Get(int id);
        IDataResult<List<TDepartment>> GetList();
    }
}