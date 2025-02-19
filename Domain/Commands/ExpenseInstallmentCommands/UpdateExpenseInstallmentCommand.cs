using MediatR;

namespace Domain.Commands.ExpenseInstallmentCommands;

public class UpdateExpenseInstallmentCommand : ExpenseInstallmentCommand, IRequest<Unit>
{
    public UpdateExpenseInstallmentCommand(Guid id, double amount, DateTime dueDate, bool isPaid, Guid userId)
    {
        Id = id;
        Amount = amount;
        DueDate = dueDate;
        IsPaid = isPaid;
        UserId = userId;
    }
}
