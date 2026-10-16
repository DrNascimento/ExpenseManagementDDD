namespace Application.Interfaces
{
    public interface ITokenAppService
    {
        int AccessTokenExpiresInSeconds { get; }

        string GenerateAccessToken(User user);

        GeneratedRefreshToken GenerateRefreshToken();

        string HashRefreshToken(string refreshToken);
    }

    /// <summary>
    /// A new refresh token: <see cref="Token"/> goes to the client, only <see cref="TokenHash"/> is persisted.
    /// </summary>
    public record GeneratedRefreshToken(string Token, string TokenHash, DateTime CreatedAt, DateTime ExpiresAt);
}
