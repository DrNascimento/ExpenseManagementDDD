using Domain.Commands.ExpenseTypeCommands;
using Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.ExpenseTypeCommandValidations
{
    public class DeleteExpenseTypeCommandValidator : ExpenseTypeCommandValidations<DeleteExpenseTypeCommand>
    {
        public DeleteExpenseTypeCommandValidator(IExpenseTypeRepository expenseTypeRepository) 
            : base(expenseTypeRepository)
        {
            ValidateId();
        }
    }
}
