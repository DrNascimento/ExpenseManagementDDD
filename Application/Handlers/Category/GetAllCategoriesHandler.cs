using Application.DTO.Output.Category;

namespace Application.Handlers.Category;

public class GetAllCategoriesHandler(ICategoryRepository categoryRepository)
{
    public IEnumerable<CategoryOutput> Execute(Guid userId)
    {
        var categories = categoryRepository.GetUsersCategories(userId);
        return categories.ToOutput();
    }
}
