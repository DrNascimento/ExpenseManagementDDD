namespace Application.DTO.Output.ExpenseType;

public static class ExpenseTypeMappingExtensions
{
    public static ExpenseTypeOutput ToOutput(this Domain.Entities.ExpenseType expenseType) =>
        new()
        {
            Id = expenseType.Id,
            Name = expenseType.Name,
            IsFixed = expenseType.IsFixed
        };

    public static IEnumerable<ExpenseTypeOutput> ToOutput(this IEnumerable<Domain.Entities.ExpenseType> expenseTypes) =>
        expenseTypes.Select(ToOutput);
}
