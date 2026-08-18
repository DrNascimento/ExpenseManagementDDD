namespace Application.DTO.Input.ExpenseType;

public record CreateExpenseTypeInput
{
    public string Name { get; init; } = string.Empty;
    public bool IsFixed { get; init; }
}
