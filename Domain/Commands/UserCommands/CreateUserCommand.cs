using Domain.Enums;
using MediatR;

namespace Domain.Commands.UserCommands;

public class CreateUserCommand : UserCommand, IRequest<Guid>
{
    public string ConfirmPassword { get; set; }
    public CreateUserCommand(string name, string email, string password, string confirmPassword, UserTypeEnum userTypeEnum)
    {
        Name = name;
        Email = email;
        Password = password;
        ConfirmPassword = confirmPassword;
        UserTypeEnum = userTypeEnum;
    }
}
