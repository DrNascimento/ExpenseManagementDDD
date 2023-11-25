using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.ExpenseCommands
{
    public class DeleteExpenseCommand : ExpenseCommand, IRequest<Unit>
    {
        public DeleteExpenseCommand(int id) 
        { 
            Id = id;
        }
    }
}
