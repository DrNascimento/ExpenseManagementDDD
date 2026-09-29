using Application.DTO.Output.ExpenseType;

namespace Application.Handlers.ExpenseType;

public class GetExpenseTypeByIdHandler(IExpenseTypeRepository expenseTypeRepository)
{
    public async Task<ExpenseTypeOutput> ExecuteAsync(Guid id)
    {
        var expenseType = await expenseTypeRepository.GetById(id);
        return expenseType.ToOutput();
    }
}
