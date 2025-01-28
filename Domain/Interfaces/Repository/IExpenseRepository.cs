using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IExpenseRepository : IRepository<Expense>
{
    IEnumerable<Expense> GetExpenses(Guid userId);

    IQueryable<Expense> GetExpenses();
    bool HasExpenseByCategory(Guid id);
    bool HasExpenseByIdAndUser(Guid id, Guid userId);

}
