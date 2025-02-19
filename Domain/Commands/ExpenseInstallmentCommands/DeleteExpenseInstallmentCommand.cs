using MediatR;

namespace Domain.Commands.ExpenseInstallmentCommands;

public class DeleteExpenseInstallmentCommand : ExpenseInstallmentCommand, IRequest<Unit>
{
    public DeleteExpenseInstallmentCommand(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}
