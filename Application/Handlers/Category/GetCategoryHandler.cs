using Application.DTO.Output.Category;

namespace Application.Handlers.Category;

public class GetCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
{
    public async Task<CategoryOutput> ExecuteAsync(Guid id)
    {
        var category = await categoryRepository.GetById(id);
        return mapper.Map<CategoryOutput>(category);
    }
}
