using Application.DTO.Output.Account;

namespace Application.Interfaces
{
    public interface ITokenAppService
    {
        /// <summary>
        /// Issues an access token and a new refresh token, registering the refresh token on <paramref name="user"/>.
        /// </summary>
        AuthTokensOutput IssueTokens(User user);

        /// <summary>
        /// Revokes <paramref name="current"/> and issues an access token with its replacement refresh token.
        /// </summary>
        AuthTokensOutput RotateTokens(User user, RefreshToken current);

        string HashRefreshToken(string refreshToken);
    }
}
