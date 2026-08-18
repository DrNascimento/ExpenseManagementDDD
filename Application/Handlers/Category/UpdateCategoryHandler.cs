using Application.DTO.Input.Category;

namespace Application.Handlers.Category;

public class UpdateCategoryHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateCategoryInput> validator)
{
    public async Task ExecuteAsync(Guid id, UpdateCategoryInput input, Guid userId, string role)
    {
        input.Id = id;
        await validator.ValidateAndThrowAsync(input);

        var category = await ValidatePermissionAsync(id, userId, role);

        if (!categoryRepository.IsNameAvailable(input.Name, category.UserId, id))
            throw new FluentValidation.ValidationException("Category already exists.");

        category.Name = input.Name;
        categoryRepository.Update(category);
        await unitOfWork.CommitAsync();
    }

    private async Task<Domain.Entities.Category> ValidatePermissionAsync(Guid id, Guid userId, string role)
    {
        var category = await categoryRepository.GetById(id)
            ?? throw new ResourceNotFoundException("Category not found.");

        var isAdmin = role.Equals(nameof(UserTypeEnum.Admin), StringComparison.OrdinalIgnoreCase);
        if (category.UserId is null && !isAdmin)
            throw new InvalidOperationException("It's not possible edit this Category.");

        if (category.UserId is not null && category.UserId != userId)
            throw new InvalidOperationException("It's not possible edit this Category.");

        return category;
    }
}
