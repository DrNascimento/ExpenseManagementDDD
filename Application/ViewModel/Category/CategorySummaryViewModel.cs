using System;

namespace Application.ViewModel.Category;

public class CategoriesSummaryViewModel
{
    public IEnumerable<CategorySummaryViewModel> SummaryCategories { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }
}

public class CategorySummaryViewModel
{
    public string Name { get; set; }

    public double Amount { get; set; }

    public double Percent { get; set; }
}

