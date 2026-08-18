using Application.DTO.Input.Account;

namespace Application.Handlers.Account;

public class CreateAccountHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateAccountInput> validator)
{
    public async Task<Guid> ExecuteAsync(CreateAccountInput input)
    {
        await validator.ValidateAndThrowAsync(input);

        var user = new Domain.Entities.User
        {
            Name = input.Name,
            Email = input.Email,
            Password = BCryptHash.HashPassword(input.Password),
            UserTypeEnum = (UserTypeEnum)input.UserTypeEnum
        };

        userRepository.Add(user);
        await unitOfWork.CommitAsync();

        return user.Id;
    }
}
