namespace Application.DTO.Input.Account;

public record RefreshTokenInput
{
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; init; } = string.Empty;
}
