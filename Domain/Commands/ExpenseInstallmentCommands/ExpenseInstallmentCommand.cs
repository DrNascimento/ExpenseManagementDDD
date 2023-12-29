using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.ExpenseInstallmentCommands
{
    public class ExpenseInstallmentCommand
    {
        public int Id { get; protected set; }

        public double Amount { get; protected set; }

        public DateTime DueDate { get; protected set; }

        public bool IsPaid { get; protected set; }

        public int UserId { get; protected set; }

    }
}
