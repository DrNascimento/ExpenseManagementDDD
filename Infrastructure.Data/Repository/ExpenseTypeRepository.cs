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

        public bool IsNameAvailable(string name, int exceptId = 0)
        {
            return !DbSet.Any(x =>
                x.Name == name 
                && (exceptId <= 0 || x.Id != exceptId)
                && !x.IsDeleted);
        }

    }
}
