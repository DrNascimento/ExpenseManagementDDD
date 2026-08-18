namespace Application.DTO.Input.Category;

public record CreateCategoryInput
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}
