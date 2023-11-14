using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : EntityBase
    {
        [Required]
        public string Name { get; set; }
        
        public User User { get; set; }

        public int? UserId { get; set; }
    }
}
