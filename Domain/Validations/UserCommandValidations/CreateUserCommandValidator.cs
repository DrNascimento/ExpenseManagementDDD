using Domain.Commands.UserCommands;
using Domain.Interfaces.Repository;

namespace Domain.Validations.UserCommandValidations
{
    public class CreateUserCommandValidator : UserCommandValidations<CreateUserCommand>
    {
        public CreateUserCommandValidator(IUserRepository userRepository)
            : base(userRepository)
        {
            ValidateName();
            ValidateEmail();
            ValidateEmailAvailability();
            ValidatePassword();
        }
    }
}