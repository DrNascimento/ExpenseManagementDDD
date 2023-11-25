using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        IEnumerable<Expense> GetExpenses(int userId);

        IQueryable<Expense> GetExpenses();

    }
}
