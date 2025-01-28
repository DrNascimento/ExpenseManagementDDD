using Domain.Commands.Category;
using Domain.Interfaces.Repository;
using FluentValidation;

namespace Domain.Validations.CategoryCommandValidations;

public class CategoryCommandValidations<T> : AbstractValidator<T> where T : CategoryCommand
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IExpenseRepository _expenseRepository;

    public CategoryCommandValidations(ICategoryRepository categoryRepository, IExpenseRepository expenseRepository)
    {
        _categoryRepository = categoryRepository;
        _expenseRepository = expenseRepository;
    }

    protected void ValidateId() =>
        RuleFor(c => c.Id)
            .NotEmpty();

    protected void ValidateName() =>
        RuleFor(c => c.Name)
            .NotEmpty();

    protected void ValidateNameAlreadyExists() =>
        RuleFor(c => c)
            .Must(c => _categoryRepository.IsNameAvailable(c.Name, c.UserId, c.Id))
            .WithName("Name")
            .WithMessage("This name is already in use");

    protected void ValidateUse()
    {
        RuleFor(c => c)
            .Must(c => !_expenseRepository.HasExpenseByCategory(c.Id))
            .WithName("Name")
            .WithName("You can't delete because this Category is being used for one or more Expenses");
    }
        
}
