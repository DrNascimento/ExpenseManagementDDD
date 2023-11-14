using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserExpanse : EntityBase
    {
        public UserExpanse() { }

        public User User { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public ExpanseType ExpanseType { get; set; }

        [Required]
        public int ExpanseTypeId { get;set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public bool IsPaid { get; set; }

        public UserExpanse UserExpanseParent { get; set; }

        public int? UserExpanseParentId { get; set; }

        public int? Installments { get; set; }

        public Category Category { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
