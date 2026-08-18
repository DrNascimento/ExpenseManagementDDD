namespace Application.DTO.Input.Account;

public record CreateAccountInput
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; init; } = string.Empty;

    [JsonPropertyName("confirm_password")]
    public string ConfirmPassword { get; init; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;

    [JsonPropertyName("type_user")]
    public int UserTypeEnum { get; init; }
}
