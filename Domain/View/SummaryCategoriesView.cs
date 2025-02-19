using System;

namespace Domain.View;

public class SummaryCategoriesView
{
    public IEnumerable<SummaryCategoryView> SummaryCategories { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }
}

public class SummaryCategoryView
{
    public string Name { get; set; }

    public double Amount { get; set; }

    public double Percent { get; set; }
}