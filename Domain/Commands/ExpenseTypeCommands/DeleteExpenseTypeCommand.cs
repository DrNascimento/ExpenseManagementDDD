using MediatR;

namespace Domain.Commands.ExpenseTypeCommands;

public class DeleteExpenseTypeCommand : ExpenseTypeCommand, IRequest<Unit>
{
    public DeleteExpenseTypeCommand(Guid id)
    {
        Id = id;
    }
}