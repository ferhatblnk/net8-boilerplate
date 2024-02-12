using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class UserDto : IDto
    {
        public Guid? CompanyId { get; set; }
        public Guid? RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiredAt { get; set; }

    }
}
