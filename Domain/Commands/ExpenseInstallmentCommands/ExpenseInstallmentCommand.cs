using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.ExpenseInstallmentCommands
{
    public class ExpenseInstallmentCommand
    {
        public int Id { get; set; }

        public double Amount { get; set; }

        public bool IsPaid { get; set; }

        
    }
}
