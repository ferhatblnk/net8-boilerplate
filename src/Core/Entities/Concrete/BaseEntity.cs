using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete
{
    public class BaseEntity : IEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid RowGuid { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool Deleted { get; set; }
    }
}
