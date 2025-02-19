
using Domain.Commands.ExpenseTypeCommands;
using Domain.Interfaces.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.ExpenseTypeCommandValidations
{
    public class ExpenseTypeCommandValidations<T> :
        AbstractValidator<T>
        where T : ExpenseTypeCommand
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;

        public ExpenseTypeCommandValidations(IExpenseTypeRepository expenseTypeRepository) 
        {
            _expenseTypeRepository = expenseTypeRepository;
        }

        protected void ValidateId() =>
            RuleFor(x => x.Id)
                .NotEmpty();

        protected void ValidateName() =>
            RuleFor(x => x.Name)
                .NotEmpty();

        protected void ValidateNameAlreadyExists() => 
            RuleFor(x => x)
                .Must(x => _expenseTypeRepository.IsNameAvailable(x.Name, x.Id))
                .WithName("Name")
                .WithMessage("This name is already in use");
    }
}
