using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExpanseType : EntityBase
    {
        [MaxLength(32)]
        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsFixed { get; set; }
    }
}
