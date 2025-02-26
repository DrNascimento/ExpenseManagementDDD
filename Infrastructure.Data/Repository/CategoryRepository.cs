using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.View;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ExpenseManagementContext context) : base(context)
    {
    }

    public bool IsNameAvailable(string name, Guid? userId, Guid? exceptId)
    {
        return !DbSet.Any(x =>
            x.Name == name
            && (userId.HasValue || x.UserId == userId)
            && (!exceptId.HasValue || x.Id != exceptId)
            && !x.IsDeleted);
    }

    public IEnumerable<Category> GetUsersCategories(Guid userId)
    {
        return DbSet
            .Where(c => !c.IsDeleted
                && (c.UserId == null || c.UserId == userId))
            .OrderBy(c => c.Name);
    }

    public async Task<SummaryCategoriesView> Summary(DateTime start, DateTime end, Guid userId)
    {

        var usersExpenses =  Db.ExpenseInstallments.Include(x => x.Expense)
            .Where(x => x.Expense.UserId == userId && x.DueDate >= start && x.DueDate <= end && !x.IsDeleted).
            Select(x => new { x.Expense.Category, x.Amount});

        double sumExpenses = await usersExpenses.SumAsync(f => f.Amount);

        return new SummaryCategoriesView
        {
            Start = start,
            End = end,
            SummaryCategories = usersExpenses
                .GroupBy(f => f.Category.Name)
                .Select(g => new SummaryCategoryView
                {
                    Name = g.Key,
                    Amount = g.Sum(x => x.Amount),
                    Percent = sumExpenses > 0 ? (g.Sum(x => x.Amount) / sumExpenses) * 100 : 0
                })
        };
    }
}
