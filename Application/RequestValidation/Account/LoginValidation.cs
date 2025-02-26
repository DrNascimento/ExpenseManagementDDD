using Application.ViewModel.Account;
using FluentValidation;

namespace Application.RequestValidation.Account;

public class LoginValidation : AbstractValidator<LoginViewModel>
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
