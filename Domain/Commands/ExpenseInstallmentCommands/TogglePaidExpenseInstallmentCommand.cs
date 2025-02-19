using MediatR;

namespace Domain.Commands.ExpenseInstallmentCommands;

public class TogglePaidExpenseInstallmentCommand : ExpenseInstallmentCommand, IRequest<Unit>
{
    public TogglePaidExpenseInstallmentCommand(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}
