using Domain.Entities;
using Domain.View;

namespace Domain.Interfaces.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        bool IsNameAvailable(string name, Guid? userId, Guid? exceptId);

        IEnumerable<Category> GetUsersCategories(Guid userId);

        Task<SummaryCategoriesView> Summary(DateTime start, DateTime end, Guid userId);
    }
}
