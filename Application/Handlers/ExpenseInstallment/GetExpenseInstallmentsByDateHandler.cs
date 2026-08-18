using Application.DTO.Output.ExpenseInstallment;

namespace Application.Handlers.ExpenseInstallment;

public class GetExpenseInstallmentsByDateHandler(IExpenseInstallmentRepository expenseInstallmentRepository, IMapper mapper)
{
    public IEnumerable<ExpenseInstallmentOutput> Execute(int year, int month, int day, Guid userId)
    {
        var installments = expenseInstallmentRepository
            .GetExpenseInstallments(userId)
            .Where(e => e.DueDate.Year == year
                && (month == 0 || e.DueDate.Month == month)
                && (day == 0 || e.DueDate.Day == day))
            .OrderBy(e => e.InstallmentNumber);

        return mapper.Map<IEnumerable<ExpenseInstallmentOutput>>(installments);
    }
}
