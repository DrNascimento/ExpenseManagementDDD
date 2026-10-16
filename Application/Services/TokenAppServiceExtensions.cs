using Application.DTO.Output.Account;
using Application.Interfaces;

namespace Application.Services;

public static class TokenAppServiceExtensions
{
    /// <summary>
    /// Issues a new access token and refresh token for <paramref name="user"/>, registering the refresh token on the user.
    /// </summary>
    public static AuthTokensOutput IssueTokens(this ITokenAppService tokenAppService, User user)
    {
        var refreshToken = tokenAppService.GenerateRefreshToken();
        user.AddRefreshToken(refreshToken.TokenHash, refreshToken.CreatedAt, refreshToken.ExpiresAt);

        return tokenAppService.ToOutput(user, refreshToken);
    }

    public static AuthTokensOutput ToOutput(this ITokenAppService tokenAppService, User user, GeneratedRefreshToken refreshToken) =>
        new(tokenAppService.GenerateAccessToken(user), refreshToken.Token, tokenAppService.AccessTokenExpiresInSeconds);
}
