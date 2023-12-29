using Domain.Commands.ExpenseInstallmentCommands;
using Domain.Interfaces.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.ExpenseInstallmentCommandValidations
{
    public class ExpenseInstallmentCommandValidations <T> : AbstractValidator<T> where T : ExpenseInstallmentCommand
    {
        private readonly IExpenseInstallmentRepository _expenseInstallmentRepository;
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseInstallmentCommandValidations(IExpenseInstallmentRepository expenseInstallmentRepository, IExpenseRepository expenseRepository)
        {
            _expenseInstallmentRepository = expenseInstallmentRepository;
            _expenseRepository = expenseRepository;
        }

        public void ValidateId() => 
            RuleFor(x => x.Id)
                .NotEmpty();

        public void ValidateAmount() => 
            RuleFor(x => x.Amount)
                .NotEmpty()
                .GreaterThan(0);

        public void ValidateDueDate() =>
            RuleFor(x => x)
                .Must(x => !_expenseInstallmentRepository.HasOneInstallmentByMonth(x.Id, x.DueDate))
                .WithMessage("There is already installment in the selected month");

        public void ValidateUserId() =>
            RuleFor(x => x.UserId)
                .NotEmpty();

        public void ValidateBelongs() =>
            RuleFor(x => x)
                .Must(x => !_expenseRepository.HasExpenseByIdAndUser(x.Id, x.UserId))
                .WithMessage("Installment not found");


    }
}

