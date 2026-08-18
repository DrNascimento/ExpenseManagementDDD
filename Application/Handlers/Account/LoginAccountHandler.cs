using Application.DTO.Input.Account;
using Application.Exceptions;
using Application.Interfaces;
using Application.RequestValidation.Account;

namespace Application.Handlers.Account;

public class LoginAccountHandler(IUserRepository userRepository, ITokenAppService tokenAppService)
{
    public async Task<string> ExecuteAsync(LoginInput input)
    {
        new LoginValidation().ValidateAndThrow(input);

        var user = await userRepository.GetByEmail(input.Email);

        if (user is null || !BCryptHash.VerifyPassword(input.Password, user.Password))
            throw new LoginFailedException();

        return tokenAppService.GenerateToken(user);
    }
}
