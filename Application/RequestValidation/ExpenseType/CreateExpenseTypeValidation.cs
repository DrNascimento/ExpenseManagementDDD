using Application.DTO.Input.ExpenseType;

namespace Application.RequestValidation.ExpenseType;

public class CreateExpenseTypeValidation : AbstractValidator<CreateExpenseTypeInput>
{
    public CreateExpenseTypeValidation(IExpenseTypeRepository expenseTypeRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(32)
            .Must(name => expenseTypeRepository.IsNameAvailable(name, null))
            .WithMessage("Expense type already exists.");
    }
}
