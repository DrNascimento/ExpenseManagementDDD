using Application.ViewModel.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IExpenseAppService : IDisposable
    {
        Task<int> Create(CreateExpenseViewModel createExpenseViewModel);

        Task<ExpenseViewModel> Get(int id);

        IEnumerable<ExpenseViewModel> GetAll();

        Task Update(int id, UpdateExpenseViewModel updateExpenseViewModel);

        Task Delete(int id);
    }
}
