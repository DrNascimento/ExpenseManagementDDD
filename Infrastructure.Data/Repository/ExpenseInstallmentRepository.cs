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
            await DbSet
                .Where(x => x.ExpenseId == expenseId)
                .ExecuteUpdateAsync(x => 
                    x.SetProperty(e => e.IsDeleted, e => true)
                );

            SaveChanges();
        }

        public async Task<ExpenseInstallment> GetExpenseInstallment(int id, int userId)
        {
            return await GetIncludes()
                .FirstOrDefaultAsync(e =>
                    !e.IsDeleted 
                    && e.Id == id
                    && e.Expense.UserId == userId);
        }

        public IEnumerable<ExpenseInstallment> GetExpenseInstallments(int userId)
        {
            return GetIncludes()
                .Where(e => !e.IsDeleted 
                    && e.Expense.UserId == userId);
        }

        public bool HasByUserIdAndId(int id, int userId)
        {
            return DbSet.Any(e =>
                !e.IsDeleted
                && e.Id == userId
                && e.Expense.UserId == userId);
        }

        public bool HasOneInstallmentByMonth(int exceptId, DateTime dueDate) 
        {
            return DbSet.Any(e => 
                !e.IsDeleted
                && e.Id != exceptId 
                && e.DueDate.Year == dueDate.Year
                && e.DueDate.Month == dueDate.Month
                && DbSet.First(e1 => e1.Id == exceptId).ExpenseId == e.ExpenseId);                     
        }

        private IQueryable<ExpenseInstallment> GetIncludes() =>
            DbSet
                .Include(e => e.Expense)
                    .ThenInclude(e => e.Category)
                .Include(e => e.Expense)
                    .ThenInclude(e => e.ExpenseType);
    }
}
