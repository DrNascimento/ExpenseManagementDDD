namespace Application.DTO.Output.Category;

public class CategoriesSummaryOutput
{
    public IEnumerable<CategorySummaryOutput> SummaryCategories { get; set; } = [];
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

public class CategorySummaryOutput
{
    public string Name { get; set; } = string.Empty;
    public double Amount { get; set; }
    public double Percent { get; set; }
}
