using Application.DTO.Output.ExpenseInstallment;

namespace Application.Handlers.ExpenseInstallment;

public class GetExpenseInstallmentHandler(IExpenseInstallmentRepository expenseInstallmentRepository, IMapper mapper)
{
    public async Task<ExpenseInstallmentOutput> ExecuteAsync(Guid id, Guid userId)
    {
        var installment = await expenseInstallmentRepository.GetExpenseInstallment(id, userId);
        return mapper.Map<ExpenseInstallmentOutput>(installment);
    }
}
