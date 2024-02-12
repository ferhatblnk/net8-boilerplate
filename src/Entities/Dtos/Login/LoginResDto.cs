using Core.Entities;
using System;

namespace Entities.Dtos
{
    public class LoginResDto : IDto
    {
        public int Id { get; set; }
        public Guid RowGuid { get; set; }

        public string Email { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public string Company { get; set; }
    }
}
