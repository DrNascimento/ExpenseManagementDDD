using Application.ViewModel.Expense;

namespace Application.Interfaces;

public interface IExpenseAppService : IDisposable
{
    Task<Guid> Create(CreateExpenseViewModel createExpenseViewModel);

    Task<ExpenseViewModel> Get(Guid Id);

    IEnumerable<ExpenseViewModel> GetAll();

    Task Update(Guid Id, UpdateExpenseViewModel updateExpenseViewModel);

    Task Delete(Guid Id);
}
