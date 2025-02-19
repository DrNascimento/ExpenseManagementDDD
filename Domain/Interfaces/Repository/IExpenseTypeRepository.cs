using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IExpenseTypeRepository : IRepository<ExpenseType>
{
    bool IsNameAvailable(string name, Guid? exceptId);
}
