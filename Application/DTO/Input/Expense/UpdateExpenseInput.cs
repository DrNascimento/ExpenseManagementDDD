namespace Application.DTO.Input.Expense;

public record UpdateExpenseInput
{
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("category_id")]
    public Guid CategoryId { get; init; }
}
