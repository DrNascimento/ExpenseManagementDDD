using Domain.Commands.Category;
using Domain.Interfaces.Repository;

namespace Domain.Validations.CategoryCommandValidations;

public class DeleteCategoryCommandValidator : CategoryCommandValidations<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator(ICategoryRepository categoryRepository) 
        : base(categoryRepository)
    {
        ValidateId();
        ValidateUse();
    }
}
