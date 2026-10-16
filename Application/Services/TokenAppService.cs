using System.Security.Cryptography;
using Application.Interfaces;

namespace Application.Services
{
    public class TokenAppService(IOptions<JwtSettings> options, TimeProvider timeProvider) : ITokenAppService
    {
        private const int RefreshTokenSizeInBytes = 64;

        private readonly JwtSettings _jwtSettings = options.Value;

        public int AccessTokenExpiresInSeconds => _jwtSettings.AccessTokenExpirationInMinutes * 60;

        public string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = timeProvider.GetUtcNow().UtcDateTime;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.UserTypeEnum.ToString().ToLower())
                }),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                IssuedAt = now,
                NotBefore = now,
                Expires = now.AddMinutes(_jwtSettings.AccessTokenExpirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }

        public GeneratedRefreshToken GenerateRefreshToken()
        {
            var now = timeProvider.GetUtcNow().UtcDateTime;
            var token = Base64UrlEncoder.Encode(RandomNumberGenerator.GetBytes(RefreshTokenSizeInBytes));

            return new GeneratedRefreshToken(
                token,
                HashRefreshToken(token),
                now,
                now.AddDays(_jwtSettings.RefreshTokenExpirationInDays));
        }

        public string HashRefreshToken(string refreshToken) =>
            Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));
    }
}
