using Application.DTO.Output.Category;

namespace Application.Handlers.Category;

public class GetAllCategoriesHandler(ICategoryRepository categoryRepository, IMapper mapper)
{
    public IEnumerable<CategoryOutput> Execute(Guid userId)
    {
        var categories = categoryRepository.GetUsersCategories(userId);
        return mapper.Map<IEnumerable<CategoryOutput>>(categories);
    }
}
