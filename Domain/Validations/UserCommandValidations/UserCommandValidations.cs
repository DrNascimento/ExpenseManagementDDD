using Domain.Commands.UserCommands;
using Domain.Interfaces.Repository;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Domain.Validations.UserCommandValidations;

public abstract class UserCommandValidations<T>(IUserRepository userRepository) :
    AbstractValidator<T> 
    where T : UserCommand
{
    private readonly IUserRepository _userRepository = userRepository;

    private string CurrentMessage { get; set; }

    public void ValidateId() =>
        RuleFor(x => x.Id)
            .NotEmpty();

    public void ValidateName() => 
        RuleFor(x => x.Name) 
            .NotEmpty()
            .Length(1, 100);

    public void ValidateEmail() =>
        RuleFor(x => x.Email)
            .NotEmpty()
            .Length(1, 100)
            .EmailAddress();

    public void ValidateEmailAvailability() =>
        RuleFor(x => x.Email)
            .Must(_userRepository.IsEmailAvailable)
                .WithMessage("Email is not available");

    public void ValidatePassword() =>
        RuleFor(x => x.Password)
            .Must(IsStrongPassword)
                .WithMessage((_) => CurrentMessage);

    public void ValidateExisting() =>
        RuleFor(x => x.Id)
            .NotEmpty()
            .Must(_userRepository.HasUserById)
            .WithMessage("User does not exist");

    private bool IsStrongPassword(string password)
    {
        List<string> validationErrors = [];

        if (password is null)
        {
            CurrentMessage = "The password must have at least 8 characters.";
            return false;
        }

        if (password.Length < 8)
            validationErrors.Add("The password must have at least 8 characters.");

        if (!Regex.IsMatch(password, @"[A-Z]"))
            validationErrors.Add("The password must contain at least one uppercase letter.");

        if (!Regex.IsMatch(password, @"[a-z]"))
            validationErrors.Add("The password must contain at least one lowercase letter.");

        if (!Regex.IsMatch(password, @"[0-9]"))
            validationErrors.Add("The password must contain at least one digit.");

        if (!Regex.IsMatch(password, @"[@#$%^&+=]"))
            validationErrors.Add("The password must contain at least one special character.");

        if (validationErrors.Count > 0)
        {
            CurrentMessage = string.Join(" ", validationErrors);
            return false;
        }

        return true;
    }

}
