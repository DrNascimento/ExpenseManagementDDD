using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IExpenseInstallmentRepository : IRepository<ExpenseInstallment>
{
    Task DeleteByExpenseId(Guid expenseId);

    IEnumerable<ExpenseInstallment> GetExpenseInstallments(Guid userId);

    Task<ExpenseInstallment> GetExpenseInstallment(Guid id, Guid userId);

    bool HasByUserIdAndId(Guid id, Guid userId);

    bool HasOneInstallmentByMonth(Guid exceptId, DateTime dueDate);
}
