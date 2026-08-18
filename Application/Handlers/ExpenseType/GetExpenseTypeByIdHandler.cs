using Application.DTO.Output.ExpenseType;

namespace Application.Handlers.ExpenseType;

public class GetExpenseTypeByIdHandler(IExpenseTypeRepository expenseTypeRepository, IMapper mapper)
{
    public async Task<ExpenseTypeOutput> ExecuteAsync(Guid id)
    {
        var expenseType = await expenseTypeRepository.GetById(id);
        return mapper.Map<ExpenseTypeOutput>(expenseType);
    }
}
