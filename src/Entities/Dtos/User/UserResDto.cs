using Core.Entities;
using System;

namespace Entities.Dtos
{
    public class UserResDto : IDto
    {
        public int Id { get; set; }
        public Guid RowGuid { get; set; }
        public Guid? ParentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
