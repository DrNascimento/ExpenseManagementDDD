using Application.DTO.Output.ExpenseType;

namespace Application.Handlers.ExpenseType;

public class GetAllExpenseTypesHandler(IExpenseTypeRepository expenseTypeRepository)
{
    public IEnumerable<ExpenseTypeOutput> Execute()
    {
        var expenseTypes = expenseTypeRepository.GetAll();
        return expenseTypes.ToOutput();
    }
}
