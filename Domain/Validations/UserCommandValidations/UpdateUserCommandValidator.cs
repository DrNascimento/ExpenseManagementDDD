using Domain.Commands.UserCommands;
using Domain.Interfaces.Repository;

namespace Domain.Validations.UserCommandValidations;

public class UpdateUserCommandValidator : UserCommandValidations<UpdateUserCommand>
{
    public UpdateUserCommandValidator(IUserRepository userRepository) : base(userRepository)
    {
        ValidateId();
        ValidateName();
        ValidateEmail();
    }
}
