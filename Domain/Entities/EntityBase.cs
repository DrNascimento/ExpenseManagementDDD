using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
