using Application.DTO.Output.Category;

namespace Application.Handlers.Category;

public class GetCategoryHandler(ICategoryRepository categoryRepository)
{
    public async Task<CategoryOutput> ExecuteAsync(Guid id)
    {
        var category = await categoryRepository.GetById(id);
        return category.ToOutput();
    }
}
