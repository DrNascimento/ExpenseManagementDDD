using MediatR;

namespace Domain.Commands.ExpenseCommands;

public class CreateExpenseCommand : ExpenseCommand, IRequest<Guid>
{
    public CreateExpenseCommand(Guid userId, string name, Guid expenseTypeid,
        Guid categoryId, int installments, double installmentAmmount, DateTime installmentDueDate) 
    {
        UserId = userId;
        Name = name;
        ExpenseTypeId = expenseTypeid;
        CategoryId = categoryId;
        Installments = installments;
        ExpenseInstallmentAmmount = installmentAmmount;
        ExpenseInstallmentDueDate = installmentDueDate;
    }
}
