using Application.ViewModel.ExpenseInstallment;

namespace Application.Interfaces;

public interface IExpenseInstallmentAppService : IDisposable
{
    Task<ExpenseInstallmentViewModel> Get(Guid Id);
    IEnumerable<ExpenseInstallmentViewModel> GetByDate(int year, int month, int day);
    Task Update(Guid Id, UpdateExpenseInstallmentViewModel updateExpenseInstallmentViewModel);
    Task UpdatePaid(Guid Id);
    Task Delete(Guid Id);
}
