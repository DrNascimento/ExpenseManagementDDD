using System.Security.Cryptography;
using Application.DTO.Output.Account;
using Application.Interfaces;

namespace Application.Services
{
    public class TokenAppService(IOptions<JwtSettings> options, TimeProvider timeProvider) : ITokenAppService
    {
        private const int RefreshTokenSizeInBytes = 64;

        private readonly JwtSettings _jwtSettings = options.Value;

        public AuthTokensOutput IssueTokens(User user)
        {
            var now = timeProvider.GetUtcNow().UtcDateTime;
            var refreshToken = GenerateRefreshToken();

            user.AddRefreshToken(HashRefreshToken(refreshToken), now, RefreshTokenExpiresAt(now));

            return CreateOutput(user, refreshToken, now);
        }

        public AuthTokensOutput RotateTokens(User user, RefreshToken current)
        {
            var now = timeProvider.GetUtcNow().UtcDateTime;
            var refreshToken = GenerateRefreshToken();

            user.RotateRefreshToken(current, HashRefreshToken(refreshToken), now, RefreshTokenExpiresAt(now));

            return CreateOutput(user, refreshToken, now);
        }

        public string HashRefreshToken(string refreshToken) =>
            Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));

        private AuthTokensOutput CreateOutput(User user, string refreshToken, DateTime now) =>
            new(GenerateAccessToken(user, now), refreshToken, _jwtSettings.AccessTokenExpirationInMinutes * 60);

        private DateTime RefreshTokenExpiresAt(DateTime now) => now.AddDays(_jwtSettings.RefreshTokenExpirationInDays);

        private static string GenerateRefreshToken() =>
            Base64UrlEncoder.Encode(RandomNumberGenerator.GetBytes(RefreshTokenSizeInBytes));

        private string GenerateAccessToken(User user, DateTime now)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

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
    }
}
