using Core.Entities.Concrete;
using Entities.Concrete.Membership;
using Entities.Concrete.UserAddress;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class TUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiredAt { get; set; }
        
        public List<TUserRole> UserRoles { get; set; }
        public List<TUserDepartment> UserDepartments { get; set; }
        public TUserAddress UserAddress { get; set; }
    }
}
