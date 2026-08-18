using Application.DTO.Input.Expense;

namespace Application.RequestValidation.Expense;

public class UpdateExpenseValidation : AbstractValidator<UpdateExpenseInput>
{
    public UpdateExpenseValidation(ICategoryRepository categoryRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .MustAsync(async (id, cancellationToken) => await categoryRepository.GetById(id) is not null)
            .WithMessage("Category not found.");
    }
}
