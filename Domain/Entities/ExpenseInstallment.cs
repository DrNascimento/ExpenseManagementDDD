using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExpenseInstallment : EntityBase
    {
        public Expense Expense { get; set; }

        [Required]
        public Guid ExpenseId { get; set; }

        [Required]
        public int InstallmentNumber { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public bool IsPaid { get; set; }
    }
}
