using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IExpenseRepository : IRepository<Expense>
{
    IEnumerable<Expense> GetExpenses(Guid userId);
    IEnumerable<Expense> GetByDate(int year, int month, int day);
    bool HasExpenseByCategory(Guid id);
    bool HasExpenseByIdAndUser(Guid id, Guid userId);

}
