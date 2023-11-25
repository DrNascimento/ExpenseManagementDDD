using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository
{
    public class ExpenseInstallmentRepository : Repository<ExpenseInstallment>, IExpenseInstallmentRepository
    {
        public ExpenseInstallmentRepository(ExpenseManagementContext context) : base(context)
        {
        }

        public async Task DeleteByExpenseId(int expenseId)
        {
            await Db.ExpenseInstallments
                .Where(x => x.ExpenseId == expenseId)
                .ExecuteUpdateAsync(x => 
                    x.SetProperty(e => e.IsDeleted, e => true)
                );

            SaveChanges();
        }
    }
}
