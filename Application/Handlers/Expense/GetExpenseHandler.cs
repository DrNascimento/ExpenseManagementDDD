using Application.DTO.Output.Expense;

namespace Application.Handlers.Expense;

public class GetExpenseHandler(IExpenseRepository expenseRepository, IMapper mapper)
{
    public async Task<ExpenseOutput> ExecuteAsync(Guid id)
    {
        var expense = await expenseRepository.GetById(id);
        return mapper.Map<ExpenseOutput>(expense);
    }
}
