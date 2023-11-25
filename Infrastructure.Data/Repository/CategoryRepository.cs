using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ExpenseManagementContext context) : base(context)
        {
        }

        public bool IsNameAvailable(string name, int? userId, int exceptId = 0)
        {
            return !DbSet.Any(x =>
                x.Name == name
                && x.UserId == userId
                && (exceptId <= 0 || x.Id != exceptId)
                && !x.IsDeleted);
        }

        public IEnumerable<Category> GetUsersCategories(int userId)
        {
            return DbSet
                .Where(c => !c.IsDeleted
                    && (c.UserId == null || c.UserId == userId))
                .OrderBy(c => c.Name);
        }
    }
}
