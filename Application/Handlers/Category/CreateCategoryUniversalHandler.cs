using Application.DTO.Input.Category;

namespace Application.Handlers.Category;

public class CreateCategoryUniversalHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateCategoryInput> validator)
{
    public async Task<Guid> ExecuteAsync(CreateCategoryInput input)
    {
        await validator.ValidateAndThrowAsync(input);

        if (!categoryRepository.IsNameAvailable(input.Name, null, null))
            throw new FluentValidation.ValidationException("Category already exists.");

        var category = new Domain.Entities.Category
        {
            Name = input.Name,
            UserId = null
        };

        categoryRepository.Add(category);
        await unitOfWork.CommitAsync();

        return category.Id;
    }
}
