using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities
{
    public class User : EntityBase
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(64)]
        [Required]
        public string Password { get; set; }

        [MaxLength(100)]
        [Required]
        public string Email { get; set; }


        [Column("TypeUser", TypeName ="nvarchar(50)")]
        public UserTypeEnum UserTypeEnum { get; set; }

        private readonly List<RefreshToken> _refreshTokens = new();

        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

        /// <summary>
        /// Creates a user through public sign-up, which is always <see cref="UserTypeEnum.Common"/>.
        /// </summary>
        public static User CreateCommon(string name, string email, string passwordHash) =>
            new()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = passwordHash,
                UserTypeEnum = UserTypeEnum.Common
            };

        public RefreshToken AddRefreshToken(string tokenHash, DateTime createdAt, DateTime expiresAt)
        {
            var refreshToken = new RefreshToken(Id, tokenHash, createdAt, expiresAt);
            _refreshTokens.Add(refreshToken);
            return refreshToken;
        }

        public RefreshToken? FindRefreshToken(string tokenHash) =>
            _refreshTokens.FirstOrDefault(t => t.TokenHash == tokenHash);

        /// <summary>
        /// Revokes <paramref name="current"/> and issues its replacement.
        /// </summary>
        public RefreshToken RotateRefreshToken(RefreshToken current, string newTokenHash, DateTime now, DateTime expiresAt)
        {
            if (!_refreshTokens.Contains(current))
                throw new InvalidOperationException("Refresh token does not belong to this user.");

            if (!current.IsActive(now))
                throw new InvalidOperationException("Only an active refresh token can be rotated.");

            var replacement = AddRefreshToken(newTokenHash, now, expiresAt);
            current.Revoke(now, replacement.Id);
            return replacement;
        }

        /// <summary>
        /// Revokes the token when it is active; unknown, expired or revoked tokens are left unchanged.
        /// </summary>
        public void RevokeRefreshToken(string tokenHash, DateTime now)
        {
            var refreshToken = FindRefreshToken(tokenHash);

            if (refreshToken is not null && refreshToken.IsActive(now))
                refreshToken.Revoke(now);
        }

        public void RevokeAllActiveRefreshTokens(DateTime now)
        {
            foreach (var refreshToken in _refreshTokens.Where(t => t.IsActive(now)))
                refreshToken.Revoke(now);
        }
    }
}
