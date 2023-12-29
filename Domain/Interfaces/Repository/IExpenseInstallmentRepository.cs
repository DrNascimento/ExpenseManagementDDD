using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IExpenseInstallmentRepository : IRepository<ExpenseInstallment>
    {
        Task DeleteByExpenseId(int expenseId);
        IEnumerable<ExpenseInstallment> GetExpenseInstallments(int userId);
        Task<ExpenseInstallment> GetExpenseInstallment(int id, int userId);
        bool HasByUserIdAndId(int id, int userId);

        bool HasOneInstallmentByMonth(int exceptId, DateTime dueDate);
    }
}
