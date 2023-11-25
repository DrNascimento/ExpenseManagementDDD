using Application.ViewModel.ExpenseType;

namespace Application.Interfaces
{
    public interface IExpenseTypeAppService : IDisposable
    {
        Task<int> Create(ExpenseTypeViewModel expenseTypeViewModel);

        Task<ExpenseTypeViewModel> GetById(int id);

        IEnumerable<ExpenseTypeViewModel> GetAll();
    }
}
