using MediatR;

namespace Domain.Commands.ExpanseTypeCommands
{
    public class DeleteExpanseTypeCommand : ExpanseTypeCommand, IRequest
    {
        public DeleteExpanseTypeCommand(int id)
        {
            Id = id;
        }
    }
}