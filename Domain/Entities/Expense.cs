using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Expense : EntityBase
    {
        public Expense() { }

        public User User { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public ExpenseType ExpenseType { get; set; }

        [Required]
        public Guid ExpenseTypeId { get;set; }

        public Category Category { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public int Installments { get; set; }

        public ICollection<ExpenseInstallment> ExpenseInstallments { get; set; } = new List<ExpenseInstallment>();
    }
}
