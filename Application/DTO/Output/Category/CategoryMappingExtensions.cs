using Domain.View;

namespace Application.DTO.Output.Category;

public static class CategoryMappingExtensions
{
    public static CategoryOutput ToOutput(this Domain.Entities.Category category) =>
        new()
        {
            Id = category.Id,
            Name = category.Name
        };

    public static IEnumerable<CategoryOutput> ToOutput(this IEnumerable<Domain.Entities.Category> categories) =>
        categories.Select(ToOutput);

    public static CategoriesSummaryOutput ToOutput(this SummaryCategoriesView view) =>
        new()
        {
            SummaryCategories = view.SummaryCategories.Select(ToOutput),
            Start = view.Start,
            End = view.End
        };

    public static CategorySummaryOutput ToOutput(this SummaryCategoryView view) =>
        new()
        {
            Name = view.Name,
            Amount = view.Amount,
            Percent = view.Percent
        };
}
