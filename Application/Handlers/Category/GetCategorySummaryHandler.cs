using Application.DTO.Output.Category;

namespace Application.Handlers.Category;

public class GetCategorySummaryHandler(ICategoryRepository categoryRepository, IMapper mapper)
{
    public async Task<CategoriesSummaryOutput> ExecuteAsync(DateTime start, DateTime end, Guid userId)
    {
        var summary = await categoryRepository.Summary(start, end, userId);
        return mapper.Map<CategoriesSummaryOutput>(summary);
    }
}
