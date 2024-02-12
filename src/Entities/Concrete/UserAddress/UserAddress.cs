using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Concrete.UserAddress
{
    public class TUserAddress : BaseEntity
    {
        public int UserId { get; set; }
        public TUser User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}