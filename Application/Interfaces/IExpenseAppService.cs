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
        Task<Guid> Create(CreateExpenseViewModel createExpenseViewModel);

        ExpenseViewModel Get(Guid Id);

        IEnumerable<ExpenseViewModel> GetAll();

        Task Update(Guid Id, UpdateExpenseViewModel updateExpenseViewModel);

        Task Delete(Guid Id);
    }
}
