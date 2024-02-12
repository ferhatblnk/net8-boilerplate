using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Concrete.Membership
{
    public class TUserRole : BaseEntity
    {
        public int UserId { get; set; }
        public TUser User { get; set; }
        public int RoleId { get; set; }
        public TRole Role { get; set; }
    }
}