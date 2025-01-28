using MediatR;

namespace Domain.Commands.UserCommands;

public class DeleteUserCommand : UserCommand, IRequest<Unit>
{
    public DeleteUserCommand(Guid id) 
    {
        Id = id;
    }
}
