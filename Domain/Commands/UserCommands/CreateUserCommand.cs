using Domain.Enums;
using MediatR;

namespace Domain.Commands.UserCommands
{
    public class CreateUserCommand : UserCommand, IRequest<int>
    {
        public CreateUserCommand(string name, string email, string password, UserTypeEnum userTypeEnum)
        {
            Name = name;
            Password = password;
            Email = email;
            UserTypeEnum = userTypeEnum;
        }
    }
}
