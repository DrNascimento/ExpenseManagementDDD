using Application.ViewModel.ExpenseInstallment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IExpenseInstallmentAppService : IDisposable
    {
        Task<ExpenseInstallmentViewModel> Get(int id);
        IEnumerable<ExpenseInstallmentViewModel> GetByDate(int year, int month, int day);
        Task Update(int id, UpdateExpenseInstallmentViewModel updateExpenseInstallmentViewModel);
        Task UpdatePaid(int id);
        Task Delete(int id);

    }
}
