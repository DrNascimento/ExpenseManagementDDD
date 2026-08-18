using Application.DTO.Input.Expense;

namespace Application.RequestValidation.Expense;

public class CreateExpenseValidation : AbstractValidator<CreateExpenseInput>
{
    public CreateExpenseValidation(ICategoryRepository categoryRepository, IExpenseTypeRepository expenseTypeRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .MustAsync(async (id, cancellationToken) => await categoryRepository.GetById(id) is not null)
            .WithMessage("Category not found.");

        RuleFor(x => x.ExpenseTypeId)
            .NotEmpty()
            .MustAsync(async (id, cancellationToken) => await expenseTypeRepository.GetById(id) is not null)
            .WithMessage("Expense type not found.");

        RuleFor(x => x.Installments)
            .GreaterThan(0);

        RuleFor(x => x.ExpenseInstallmentAmount)
            .GreaterThan(0);

        RuleFor(x => x.ExpenseInstallmentDueDate)
            .NotEmpty();
    }
}
