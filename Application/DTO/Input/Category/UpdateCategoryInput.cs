namespace Application.DTO.Input.Category;

public record UpdateCategoryInput
{
    public Guid Id { get; set; }
    public string Name { get; init; } = string.Empty;
}
