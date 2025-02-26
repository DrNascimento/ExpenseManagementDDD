using Domain.Commands.Category;
using Domain.Interfaces.Repository;

namespace Domain.Validations.CategoryCommandValidations;

public class UpdateCategoryCommandValidator : CategoryCommandValidations<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator(ICategoryRepository categoryRepository, IExpenseRepository expenseRepository) : 
        base(categoryRepository, expenseRepository)
    {
        ValidateId();
        ValidateName();
        ValidateNameAlreadyExists();
    }
}
