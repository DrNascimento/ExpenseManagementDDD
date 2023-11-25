using Domain.Commands.ExpenseCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.Expense
{
    public class CreateExpenseCommandValidator : ExpenseCommandValidations<CreateExpenseCommand>
    {
        public CreateExpenseCommandValidator()
        {
            ValidateUserId();
            ValidateName();
            ValidateExpenseTypeId();
            ValidateCategoryId();
            ValidateInstallments();
            ValidateAmount();
            ValidateDueDate();
        }
    }
}
