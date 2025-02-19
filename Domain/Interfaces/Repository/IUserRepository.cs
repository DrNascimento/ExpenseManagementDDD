using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IUserRepository : IRepository<User>
{
    bool IsEmailAvailable(string email);

    Task<User> GetByEmailAndPassword(string email, string password);

    Task<User> GetByEmail(string email);

    bool HasUserById(Guid id);
}
