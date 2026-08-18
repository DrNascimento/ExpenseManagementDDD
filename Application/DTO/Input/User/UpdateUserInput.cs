namespace Application.DTO.Input.User;

public record UpdateUserInput
{
    public Guid Id { get; set; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public int UserTypeEnum { get; init; }
}
