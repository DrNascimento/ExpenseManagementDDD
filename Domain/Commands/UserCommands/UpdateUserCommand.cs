using Domain.Enums;
using MediatR;

namespace Domain.Commands.UserCommands;

public class UpdateUserCommand : UserCommand, IRequest<Unit>
{
    public UpdateUserCommand(Guid id, string name, string email, UserTypeEnum userTypeEnum) 
    {
        Id = id;
        Name = name;
        Email = email;
        UserTypeEnum = userTypeEnum;
    }
}
