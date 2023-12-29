using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.ExpenseInstallmentCommands
{
    public class UpdateExpenseInstallmentCommand : ExpenseInstallmentCommand, IRequest<Unit>
    {
        public UpdateExpenseInstallmentCommand(int id, double amount, DateTime dueDate, bool isPaid, int userId)
        {
            Id = id;
            Amount = amount;
            DueDate = dueDate;
            IsPaid = isPaid;
            UserId = userId;
        }
    }
}
