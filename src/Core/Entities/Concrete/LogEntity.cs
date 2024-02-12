using System;

namespace Core.Entities.Concrete
{
    public class LogEntity : BaseEntity
    {
        public Guid GroupId { get; set; }
        public Guid? LogTypeId { get; set; }
        public Guid? LogUserId { get; set; }
    }
}
