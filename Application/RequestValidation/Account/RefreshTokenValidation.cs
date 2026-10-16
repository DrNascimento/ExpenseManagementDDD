using Application.DTO.Input.Account;

namespace Application.RequestValidation.Account;

public class RefreshTokenValidation : AbstractValidator<RefreshTokenInput>
{
    public RefreshTokenValidation()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}
