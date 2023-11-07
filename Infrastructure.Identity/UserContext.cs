using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.CrossCutting.Identity
{
    public interface IUserContext
    {
        string UserId { get; }
        string UserName { get; }
        string Role { get; }
    }

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId
            => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public string UserName =>
            _httpContextAccessor.HttpContext?.User.Identity?.Name;

        public string Role =>
            _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

    }
}