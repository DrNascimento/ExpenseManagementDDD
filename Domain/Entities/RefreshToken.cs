namespace Domain.Entities;

/// <summary>
/// Refresh token issued to a <see cref="User"/>. Only the SHA-256 hash of the token is stored.
/// Created and revoked through the owning <see cref="User"/>.
/// </summary>
public class RefreshToken
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public string TokenHash { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    public DateTime? RevokedAt { get; private set; }

    public Guid? ReplacedByTokenId { get; private set; }

    public bool IsRevoked => RevokedAt is not null;

    private RefreshToken() { }

    internal RefreshToken(Guid userId, string tokenHash, DateTime createdAt, DateTime expiresAt)
    {
        if (string.IsNullOrWhiteSpace(tokenHash))
            throw new ArgumentException("Token hash is required.", nameof(tokenHash));

        if (expiresAt <= createdAt)
            throw new ArgumentException("Expiration must be after creation.", nameof(expiresAt));

        Id = Guid.NewGuid();
        UserId = userId;
        TokenHash = tokenHash;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }

    public bool IsExpired(DateTime now) => now >= ExpiresAt;

    public bool IsActive(DateTime now) => !IsRevoked && !IsExpired(now);

    internal void Revoke(DateTime revokedAt, Guid? replacedByTokenId = null)
    {
        RevokedAt = revokedAt;
        ReplacedByTokenId = replacedByTokenId;
    }
}
