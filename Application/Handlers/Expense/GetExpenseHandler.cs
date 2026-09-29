using Application.DTO.Output.Expense;

namespace Application.Handlers.Expense;

public class GetExpenseHandler(IExpenseRepository expenseRepository)
{
    public async Task<ExpenseOutput> ExecuteAsync(Guid id)
    {
        var expense = await expenseRepository.GetById(id);
        return expense.ToOutput();
    }
}
