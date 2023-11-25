using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.ExpenseTypeCommands
{
    public class UpdateExpenseTypeCommand : ExpenseTypeCommand, IRequest<Unit>
    {
        public UpdateExpenseTypeCommand(int id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
