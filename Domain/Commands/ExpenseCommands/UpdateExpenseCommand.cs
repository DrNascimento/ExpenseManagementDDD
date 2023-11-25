using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.ExpenseCommands
{
    public class UpdateExpenseCommand : ExpenseCommand, IRequest<Unit>
    {
        public UpdateExpenseCommand(int id, string name, int categoryId) 
        { 
            Id = id;
            Name = name;
            CategoryId = categoryId;
        }
    }
}
