using Application.DTO.Input.Account;
using Application.DTO.Output.Account;
using Application.Exceptions;
using Application.Interfaces;
using Application.RequestValidation.Account;

namespace Application.Handlers.Account;

public class LoginAccountHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenAppService tokenAppService)
{
    public async Task<AuthTokensOutput> ExecuteAsync(LoginInput input)
    {
        new LoginValidation().ValidateAndThrow(input);

        var user = await userRepository.GetByEmail(input.Email);

        if (user is null || !BCryptHash.VerifyPassword(input.Password, user.Password))
            throw new LoginFailedException();

        var tokens = tokenAppService.IssueTokens(user);
        await unitOfWork.CommitAsync();

        return tokens;
    }
}
