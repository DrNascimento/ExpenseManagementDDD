using Domain.Commands.ExpenseTypeCommands;
using Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.ExpenseTypeCommandValidations
{
    public class UpdateExpenseTypeCommandValidator : ExpenseTypeCommandValidations<UpdateExpenseTypeCommand>
    {
        public UpdateExpenseTypeCommandValidator(IExpenseTypeRepository expenseTypeRepository) 
            : base(expenseTypeRepository)
        {
            ValidateId();
            ValidateName();
            ValidateNameAlreadyExists();
        }
    }
}
