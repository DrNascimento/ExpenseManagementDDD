using Application.DTO.Input.Category;

namespace Application.Handlers.Category;

public class CreateCategoryByUserHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateCategoryInput> validator)
{
    public async Task<Guid> ExecuteAsync(CreateCategoryInput input, Guid userId)
    {
        await validator.ValidateAndThrowAsync(input);

        if (!categoryRepository.IsNameAvailable(input.Name, userId, null))
            throw new FluentValidation.ValidationException("Category already exists.");

        var category = new Domain.Entities.Category
        {
            Name = input.Name,
            UserId = userId
        };

        categoryRepository.Add(category);
        await unitOfWork.CommitAsync();

        return category.Id;
    }
}
