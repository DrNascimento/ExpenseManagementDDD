using MediatR;

namespace Domain.Commands.ExpenseTypeCommands;

public class UpdateExpenseTypeCommand : ExpenseTypeCommand, IRequest<Unit>
{
    public UpdateExpenseTypeCommand(Guid id, string name) 
    {
        Id = id;
        Name = name;
    }
}
