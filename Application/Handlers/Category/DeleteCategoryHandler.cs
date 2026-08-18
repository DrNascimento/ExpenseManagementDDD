namespace Application.Handlers.Category;

public class DeleteCategoryHandler(
    ICategoryRepository categoryRepository,
    IExpenseRepository expenseRepository,
    IUnitOfWork unitOfWork)
{
    public async Task ExecuteAsync(Guid id, Guid userId, string role)
    {
        var category = await ValidatePermissionAsync(id, userId, role);

        if (expenseRepository.HasExpenseByCategory(id))
            throw new InvalidOperationException("It's not possible delete this Category.");

        categoryRepository.Delete(category);
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