using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        bool IsNameAvailable(string name, int? userId, int exceptId = 0);

        IEnumerable<Category> GetUsersCategories(int userId);
    }
}
