using Infrastructure.CrossCutting.Identity;

namespace WebAPI.Configuration
{
    public class AppSettings
    {
        public JwtSettings jwtSettings { get; set; }
    }
}
