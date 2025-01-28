using Application.ViewModel.ExpenseType;

namespace Application.Interfaces;

public interface IExpenseTypeAppService : IDisposable
{
    Task<Guid> Create(ExpenseTypeViewModel expenseTypeViewModel);

    Task<ExpenseTypeViewModel> GetById(Guid id);

    IEnumerable<ExpenseTypeViewModel> GetAll();
}