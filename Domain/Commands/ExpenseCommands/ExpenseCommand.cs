using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.ExpenseCommands
{
    public class ExpenseCommand
    {       
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public int ExpenseTypeId { get; set; }

        public int CategoryId { get; set; }

        public int Installments { get; set; }

        public double ExpenseInstallmentAmmount { get; set; }

        public DateTime ExpenseInstallmentDueDate { get; set; }
    }
}
