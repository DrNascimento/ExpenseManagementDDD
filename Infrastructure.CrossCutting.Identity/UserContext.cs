using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.CrossCutting.Identity
{
    public interface IUserContext
    {
        int UserId { get; }
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

        public int UserId
        {
            get
            {
                string? strId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                _ = int.TryParse(strId, out int id);
                return id;
            }
        }

        public string UserName =>
            _httpContextAccessor.HttpContext?.User.Identity?.Name;

        public string Role =>
            _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

    }
}