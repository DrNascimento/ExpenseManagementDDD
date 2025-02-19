using Domain.Commands.ExpenseInstallmentCommands;
using Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.ExpenseInstallmentCommandValidations
{
    public class DeleteExpenseInstallmentCommandValidator : ExpenseInstallmentCommandValidations<DeleteExpenseInstallmentCommand>
    {
        public DeleteExpenseInstallmentCommandValidator(IExpenseInstallmentRepository expenseInstallmentRepository, IExpenseRepository expenseRepository) 
            : base(expenseInstallmentRepository, expenseRepository)
        {
            ValidateId();
            ValidateUserId();
            ValidateBelongs();
        }
    }
}
