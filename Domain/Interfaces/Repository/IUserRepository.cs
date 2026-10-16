using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IUserRepository : IRepository<User>
{
    bool IsEmailAvailable(string email);

    Task<User> GetByEmailAndPassword(string email, string password);

    Task<User> GetByEmail(string email);

    bool HasUserById(Guid id);

    /// <summary>
    /// Returns the owner of the refresh token with <paramref name="tokenHash"/>, loading that token
    /// and every non-revoked token of the user, or null when no token matches.
    /// </summary>
    Task<User?> GetByRefreshTokenHash(string tokenHash);
}
