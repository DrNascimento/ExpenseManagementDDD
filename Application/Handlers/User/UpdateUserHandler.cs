using Application.DTO.Input.User;

namespace Application.Handlers.User;

public class UpdateUserHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateUserInput> validator)
{
    public async Task ExecuteAsync(Guid id, UpdateUserInput input)
    {
        input.Id = id;
        await validator.ValidateAndThrowAsync(input);

        var user = await userRepository.GetById(id)
            ?? throw new ResourceNotFoundException("User not found.");

        user.Name = input.Name;
        user.Email = input.Email;
        user.UserTypeEnum = (UserTypeEnum)input.UserTypeEnum;

        userRepository.Update(user);
        await unitOfWork.CommitAsync();
    }
}
