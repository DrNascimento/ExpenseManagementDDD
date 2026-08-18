using Application.DTO.Input.User;

namespace Application.RequestValidation.User;

public class UpdateUserValidation : AbstractValidator<UpdateUserInput>
{
    public UpdateUserValidation(IUserRepository userRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.UserTypeEnum)
            .Must(value => Enum.IsDefined(typeof(UserTypeEnum), value))
            .WithMessage("Invalid user type.");

        RuleFor(x => x)
            .MustAsync(async (input, cancellationToken) =>
            {
                var user = await userRepository.GetByEmail(input.Email);
                return user is null || user.Id == input.Id;
            })
            .WithMessage("E-mail already registered.");
    }
}
