namespace Infrastructure.CrossCutting.Identity
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpirationInMinutes { get; set; } = 20;
        public int RefreshTokenExpirationInDays { get; set; } = 30;
    }
}
