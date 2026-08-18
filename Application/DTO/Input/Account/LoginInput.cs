namespace Application.DTO.Input.Account;

public record LoginInput
{
    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; init; } = string.Empty;

    [JsonPropertyName("is_remember_me")]
    public bool IsRememberMe { get; init; }
}
