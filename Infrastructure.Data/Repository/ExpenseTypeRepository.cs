using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class ExpenseTypeRepository : Repository<ExpenseType>, IExpenseTypeRepository
{
    protected new readonly ExpenseManagementContext Db;
    protected new readonly DbSet<ExpenseType> DbSet;
    public ExpenseTypeRepository(ExpenseManagementContext context)
        : base(context)
    {
        Db = context;
        DbSet = Db.Set<ExpenseType>();
    }

    public bool IsNameAvailable(string name, Guid? exceptId)
    {
        return !DbSet.Any(x =>
            x.Name == name 
            && (!exceptId.HasValue || x.Id != exceptId)
            && !x.IsDeleted);
    }

}
