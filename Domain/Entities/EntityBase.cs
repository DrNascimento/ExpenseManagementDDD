using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
