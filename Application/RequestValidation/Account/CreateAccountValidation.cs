using Application.DTO.Input.Account;

namespace Application.RequestValidation.Account;

public class CreateAccountValidation : AbstractValidator<CreateAccountInput>
{
    public CreateAccountValidation(IUserRepository userRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Must(userRepository.IsEmailAvailable)
            .WithMessage("E-mail already registered.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Password)
            .WithMessage("Password confirmation does not match.");

        RuleFor(x => x.UserTypeEnum)
            .Must(value => Enum.IsDefined(typeof(UserTypeEnum), value))
            .WithMessage("Invalid user type.");
    }
}
