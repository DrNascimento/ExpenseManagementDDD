using MediatR;

namespace Domain.Commands.ExpenseCommands;

public class DeleteExpenseCommand : ExpenseCommand, IRequest<Unit>
{
    public DeleteExpenseCommand(Guid id) 
    { 
        Id = id;
    }
}
