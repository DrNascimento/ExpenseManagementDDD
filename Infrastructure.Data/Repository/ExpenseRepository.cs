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

    public async Task<Expense> GetExpenseById(Guid id)
    {
        return await GetIncludes()
            .FirstOrDefaultAsync(x => 
                x.Id == id && !x.IsDeleted);
    }

    public IEnumerable<Expense> GetExpenses(Guid userId)
    {
        return GetIncludes()
            .AsNoTracking()
            .Where(e => 
                !e.IsDeleted && e.UserId == userId);
    }

    public IQueryable<Expense> GetExpenses() =>
        GetIncludes();

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
