using Application.DTO.Output.ExpenseType;

namespace Application.Handlers.ExpenseType;

public class GetAllExpenseTypesHandler(IExpenseTypeRepository expenseTypeRepository, IMapper mapper)
{
    public IEnumerable<ExpenseTypeOutput> Execute()
    {
        var expenseTypes = expenseTypeRepository.GetAll();
        return mapper.Map<IEnumerable<ExpenseTypeOutput>>(expenseTypes);
    }
}
