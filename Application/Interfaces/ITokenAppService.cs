using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITokenAppService : IDisposable
    {
        string GenerateToken(User user);

        bool ValidateToken(string token);
    }
}
