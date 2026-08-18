using Application.DTO.Input.Account;

namespace Application.RequestValidation.Account;

public class LoginValidation : AbstractValidator<LoginInput>
{
    public LoginValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
