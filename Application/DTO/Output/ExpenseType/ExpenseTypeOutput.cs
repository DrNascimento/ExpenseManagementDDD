namespace Application.DTO.Output.ExpenseType;

public record ExpenseTypeOutput
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool IsFixed { get; init; }
}
