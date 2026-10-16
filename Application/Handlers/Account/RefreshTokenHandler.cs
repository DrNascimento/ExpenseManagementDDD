using Application.DTO.Input.Account;
using Application.DTO.Output.Account;
using Application.Exceptions;
using Application.Interfaces;
using Application.Services;

namespace Application.Handlers.Account;

public class RefreshTokenHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ITokenAppService tokenAppService,
    TimeProvider timeProvider,
    IValidator<RefreshTokenInput> validator)
{
    public async Task<AuthTokensOutput> ExecuteAsync(RefreshTokenInput input)
    {
        await validator.ValidateAndThrowAsync(input);

        var tokenHash = tokenAppService.HashRefreshToken(input.RefreshToken);
        var user = await userRepository.GetByRefreshTokenHash(tokenHash);
        var current = user?.FindRefreshToken(tokenHash);

        if (user is null || current is null)
            throw new InvalidRefreshTokenException();

        var now = timeProvider.GetUtcNow().UtcDateTime;

        // A revoked token being presented again means it was stolen or leaked: end every session of the user.
        if (current.IsRevoked)
        {
            user.RevokeAllActiveRefreshTokens(now);
            await unitOfWork.CommitAsync();
            throw new InvalidRefreshTokenException();
        }

        if (current.IsExpired(now))
            throw new InvalidRefreshTokenException();

        var refreshToken = tokenAppService.GenerateRefreshToken();
        user.RotateRefreshToken(current, refreshToken.TokenHash, now, refreshToken.ExpiresAt);

        try
        {
            await unitOfWork.CommitAsync();
        }
        catch (ConcurrencyConflictException)
        {
            // Another request rotated this token first.
            throw new InvalidRefreshTokenException();
        }

        return tokenAppService.ToOutput(user, refreshToken);
    }
}
