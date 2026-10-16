using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Test.DomainTests;

public class UserRefreshTokenTests
{
    private static readonly DateTime Now = new(2026, 10, 16, 12, 0, 0, DateTimeKind.Utc);

    private static User NewUser() => User.CreateCommon("Ana", "ana@mail.com", "hashed-password");

    [Fact]
    public void AC1_CreateCommon_AlwaysCreatesCommonUser()
    {
        var user = NewUser();

        Assert.Equal(UserTypeEnum.Common, user.UserTypeEnum);
    }

    [Fact]
    public void AC11_RotateRefreshToken_RevokesCurrentAndPointsToReplacement()
    {
        var user = NewUser();
        var current = user.AddRefreshToken("hash-1", Now, Now.AddDays(30));

        var replacement = user.RotateRefreshToken(current, "hash-2", Now.AddMinutes(5), Now.AddDays(30));

        Assert.Equal(Now.AddMinutes(5), current.RevokedAt);
        Assert.Equal(replacement.Id, current.ReplacedByTokenId);
        Assert.True(replacement.IsActive(Now.AddMinutes(5)));
        Assert.NotEqual(Guid.Empty, replacement.Id);
    }

    [Fact]
    public void AC12_RefreshToken_IsNotActive_AfterExpiresAt()
    {
        var user = NewUser();
        var token = user.AddRefreshToken("hash-1", Now, Now.AddDays(30));

        Assert.True(token.IsActive(Now.AddDays(30).AddSeconds(-1)));
        Assert.True(token.IsExpired(Now.AddDays(30)));
        Assert.False(token.IsActive(Now.AddDays(30)));
    }

    [Fact]
    public void AC11_RotateRefreshToken_Throws_WhenCurrentTokenIsNotActive()
    {
        var user = NewUser();
        var current = user.AddRefreshToken("hash-1", Now, Now.AddDays(30));
        user.RotateRefreshToken(current, "hash-2", Now, Now.AddDays(30));

        Assert.Throws<InvalidOperationException>(() =>
            user.RotateRefreshToken(current, "hash-3", Now, Now.AddDays(30)));
    }

    [Fact]
    public void AC13_RevokeAllActiveRefreshTokens_RevokesOnlyActiveTokens()
    {
        var user = NewUser();
        var alreadyRevoked = user.AddRefreshToken("hash-1", Now, Now.AddDays(30));
        user.RevokeRefreshToken("hash-1", Now);
        var expired = user.AddRefreshToken("hash-2", Now.AddDays(-40), Now.AddDays(-10));
        var active1 = user.AddRefreshToken("hash-3", Now, Now.AddDays(30));
        var active2 = user.AddRefreshToken("hash-4", Now, Now.AddDays(30));

        user.RevokeAllActiveRefreshTokens(Now.AddHours(1));

        Assert.Equal(Now, alreadyRevoked.RevokedAt);
        Assert.Null(expired.RevokedAt);
        Assert.Equal(Now.AddHours(1), active1.RevokedAt);
        Assert.Equal(Now.AddHours(1), active2.RevokedAt);
    }

    [Fact]
    public void AC15_RevokeRefreshToken_SetsRevokedAt()
    {
        var user = NewUser();
        var token = user.AddRefreshToken("hash-1", Now, Now.AddDays(30));

        user.RevokeRefreshToken("hash-1", Now.AddMinutes(1));

        Assert.Equal(Now.AddMinutes(1), token.RevokedAt);
        Assert.Null(token.ReplacedByTokenId);
    }

    [Fact]
    public void AC16_RevokeRefreshToken_DoesNothing_WhenUnknownExpiredOrAlreadyRevoked()
    {
        var user = NewUser();
        var revoked = user.AddRefreshToken("hash-1", Now, Now.AddDays(30));
        user.RevokeRefreshToken("hash-1", Now);
        var expired = user.AddRefreshToken("hash-2", Now.AddDays(-40), Now.AddDays(-10));

        user.RevokeRefreshToken("hash-1", Now.AddHours(1));
        user.RevokeRefreshToken("hash-2", Now.AddHours(1));
        user.RevokeRefreshToken("unknown", Now.AddHours(1));

        Assert.Equal(Now, revoked.RevokedAt);
        Assert.Null(expired.RevokedAt);
    }

    [Fact]
    public void FindRefreshToken_ReturnsTokenByHash()
    {
        var user = NewUser();
        var token = user.AddRefreshToken("hash-1", Now, Now.AddDays(30));

        Assert.Same(token, user.FindRefreshToken("hash-1"));
        Assert.Null(user.FindRefreshToken("hash-x"));
    }
}
