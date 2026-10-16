using Application.DTO.Input.Account;
using Application.Interfaces;

namespace Application.Handlers.Account;

public class LogoutHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ITokenAppService tokenAppService,
    TimeProvider timeProvider,
    IValidator<RefreshTokenInput> validator)
{
    /// <summary>
    /// Revokes the refresh token. Unknown, expired or already revoked tokens are ignored.
    /// </summary>
    public async Task ExecuteAsync(RefreshTokenInput input)
    {
        await validator.ValidateAndThrowAsync(input);

        var tokenHash = tokenAppService.HashRefreshToken(input.RefreshToken);
        var user = await userRepository.GetByRefreshTokenHash(tokenHash);

        if (user is null)
            return;

        user.RevokeRefreshToken(tokenHash, timeProvider.GetUtcNow().UtcDateTime);
        await unitOfWork.CommitAsync();
    }
}
