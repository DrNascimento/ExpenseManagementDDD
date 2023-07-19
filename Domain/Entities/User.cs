﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class User : EntityBase
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [MaxLength(100)]
        [Required]
        public string Email { get; set; }
    }
}
