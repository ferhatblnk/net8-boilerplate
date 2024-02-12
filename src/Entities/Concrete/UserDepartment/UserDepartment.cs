using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class TUserDepartment : BaseEntity
    {
        public int DepartmentId { get; set; }
        public TDepartment Department { get; set; }
        public int UserId { get; set; }
        public TUser User { get; set; }
    }
}
