using Domain.Commands.UserCommands;
using Domain.Interfaces.Repository;
using FluentValidation;

namespace Domain.Validations.UserCommandValidations;

public class CreateUserCommandValidator : UserCommandValidations<CreateUserCommand>
{
    public CreateUserCommandValidator(IUserRepository userRepository)
        : base(userRepository)
    {
        ValidateName();
        ValidateEmail();
        ValidatePassword();
        ValidateConfirmPassword();
        ValidateEmailAvailability();
    }

    public void ValidateConfirmPassword() =>
            RuleFor(x => x.ConfirmPassword)
                .Must((obj, confirmPassword) => obj.Password == confirmPassword)
                .WithMessage("{PropertyName} must be equal to password.");
}