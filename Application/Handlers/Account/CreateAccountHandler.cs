using Application.DTO.Input.Account;
using Application.DTO.Output.Account;
using Application.Interfaces;
using Application.Services;

namespace Application.Handlers.Account;

public class CreateAccountHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ITokenAppService tokenAppService,
    IValidator<CreateAccountInput> validator)
{
    public async Task<AuthTokensOutput> ExecuteAsync(CreateAccountInput input)
    {
        await validator.ValidateAndThrowAsync(input);

        var user = Domain.Entities.User.CreateCommon(input.Name, input.Email, BCryptHash.HashPassword(input.Password));
        var tokens = tokenAppService.IssueTokens(user);

        userRepository.Add(user);
        await unitOfWork.CommitAsync();

        return tokens;
    }
}
