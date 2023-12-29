using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.ExpenseInstallmentCommands
{
    public class TogglePaidExpenseInstallmentCommand : ExpenseInstallmentCommand, IRequest<Unit>
    {
        public TogglePaidExpenseInstallmentCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
