    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    public interface IUserContext
    {
        string GetUserId();
        string GetUserName();
    }

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }

        public string GetUserName()
        {
            var userName = _httpContextAccessor.HttpContext?.User.Identity.Name;
            return userName;
        }
    }