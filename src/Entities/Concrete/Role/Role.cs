using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Concrete.Membership
{
    public class TRole : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public List<TUserRole> UserRoles { get; set; }
    }
}