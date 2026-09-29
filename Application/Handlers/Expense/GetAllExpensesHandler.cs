using Application.DTO.Output.Expense;

namespace Application.Handlers.Expense;

public class GetAllExpensesHandler(IExpenseRepository expenseRepository)
{
    public IEnumerable<ExpenseOutput> Execute(Guid userId)
    {
        var expenses = expenseRepository.GetExpenses(userId);
        return expenses.ToOutput();
    }
}
