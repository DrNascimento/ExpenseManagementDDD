using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class ExpenseRepository : Repository<Expense>, IExpenseRepository
{
    public ExpenseRepository(ExpenseManagementContext context) : base(context)
    {
    }

    public override async Task<Expense> GetById(Guid id)
    {
        var expense =  await GetIncludes()
            .FirstOrDefaultAsync(e => e.Id == id);

        if (expense == null)
            return null;

        expense.ExpenseInstallments = expense.ExpenseInstallments
            .OrderBy(i => i.InstallmentNumber)
            .ToList();

        return expense;
            
    }

    public IEnumerable<Expense> GetExpenses(Guid userId)
    {
        return GetIncludes()
            .AsNoTracking()
            .Where(e =>
                !e.IsDeleted && e.UserId == userId)
            .Select(e => new Expense(
                        e.Id,
                        e.Created,
                        e.Updated,
                        e.IsDeleted,
                        e.User,
                        e.UserId,
                        e.Name,
                        e.ExpenseType,
                        e.ExpenseTypeId,
                        e.Category,
                        e.CategoryId,
                        e.Installments,
                        e.ExpenseInstallments.OrderBy(x => x.InstallmentNumber).ToList())); 
            
    }

    public IEnumerable<Expense> GetByDate(int year, int month, int day)
    {
        return GetIncludes()
                    .Where(e =>
                        e.ExpenseInstallments
                            .Any(i =>
                                i.DueDate.Year == year
                                && (month < 1 || i.DueDate.Month == month)
                                && (day < 1 || i.DueDate.Day == day)))
                    .Select(e => new Expense (
                        e.Id,
                        e.Created,
                        e.Updated,
                        e.IsDeleted,
                        e.User,
                        e.UserId,
                        e.Name,
                        e.ExpenseType,
                        e.ExpenseTypeId,
                        e.Category,
                        e.CategoryId,
                        e.Installments,
                        e.ExpenseInstallments.OrderBy(x => x.InstallmentNumber).ToList()));



    }

    public bool HasExpenseByCategory(Guid id) =>
        DbSet.Any(e =>
            e.CategoryId == id &&
            !e.IsDeleted);

    /// <summary>
    /// Validates if exists a expense by id and userId
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public bool HasExpenseByIdAndUser(Guid id, Guid userId)
    {
        return DbSet.Any(e => 
            !e.IsDeleted
            && e.UserId == userId
            && e.Id == id);
    }


    private IQueryable<Expense> GetIncludes() =>
        DbSet
            .Include(x => x.Category)
            .Include(x => x.ExpenseType)
            .Include(x => x.ExpenseInstallments);
}
