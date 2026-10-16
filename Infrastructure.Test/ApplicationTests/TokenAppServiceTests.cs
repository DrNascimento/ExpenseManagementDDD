using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Application.Services;
using Domain.Entities;
using Infrastructure.CrossCutting.Identity;
using Infrastructure.Test.Support;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Test.ApplicationTests;

public class TokenAppServiceTests
{
    private static readonly DateTimeOffset Now = new(2026, 10, 16, 12, 0, 0, TimeSpan.Zero);

    private static TokenAppService NewService(JwtSettings? settings = null) =>
        new(Options.Create(settings ?? new JwtSettings
        {
            SecretKey = "a-test-secret-key-with-enough-length-for-hmac",
            Issuer = "issuer",
            Audience = "audience"
        }), new MutableTimeProvider(Now));

    [Fact]
    public void AC8_JwtSettings_DefaultsTo20MinutesAnd30Days()
    {
        var settings = new JwtSettings();

        Assert.Equal(20, settings.AccessTokenExpirationInMinutes);
        Assert.Equal(30, settings.RefreshTokenExpirationInDays);
    }

    [Fact]
    public void AC8_IssueTokens_AccessTokenExpiresAfterConfiguredMinutes()
    {
        var tokens = NewService().IssueTokens(NewUser());

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(tokens.AccessToken);

        Assert.Equal(Now.UtcDateTime.AddMinutes(20), jwt.ValidTo);
        Assert.Equal(20 * 60, tokens.ExpiresIn);
    }

    [Fact]
    public void AC8_IssueTokens_UsesConfiguredAccessTokenLifetime()
    {
        var service = NewService(new JwtSettings
        {
            SecretKey = "a-test-secret-key-with-enough-length-for-hmac",
            Issuer = "issuer",
            Audience = "audience",
            AccessTokenExpirationInMinutes = 5
        });

        var tokens = service.IssueTokens(NewUser());

        Assert.Equal(Now.UtcDateTime.AddMinutes(5), new JwtSecurityTokenHandler().ReadJwtToken(tokens.AccessToken).ValidTo);
        Assert.Equal(300, tokens.ExpiresIn);
    }

    [Fact]
    public void AC8_IssueTokens_RefreshTokenExpiresAfterConfiguredDays()
    {
        var user = NewUser();

        NewService().IssueTokens(user);

        var stored = Assert.Single(user.RefreshTokens);
        Assert.Equal(Now.UtcDateTime, stored.CreatedAt);
        Assert.Equal(Now.UtcDateTime.AddDays(30), stored.ExpiresAt);
    }

    [Fact]
    public void AC10_IssueTokens_RefreshTokenIs64RandomBytesBase64UrlEncoded()
    {
        var service = NewService();

        var first = service.IssueTokens(NewUser()).RefreshToken;
        var second = service.IssueTokens(NewUser()).RefreshToken;

        Assert.Equal(64, Base64UrlEncoder.DecodeBytes(first).Length);
        Assert.DoesNotContain('+', first);
        Assert.DoesNotContain('/', first);
        Assert.DoesNotContain('=', first);
        Assert.NotEqual(first, second);
    }

    [Fact]
    public void AC10_IssueTokens_StoresOnlySha256HashOnUser()
    {
        var service = NewService();
        var user = NewUser();

        var refreshToken = service.IssueTokens(user).RefreshToken;

        var expected = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));
        Assert.Equal(expected, Assert.Single(user.RefreshTokens).TokenHash);
        Assert.Equal(expected, service.HashRefreshToken(refreshToken));
    }

    [Fact]
    public void AC11_RotateTokens_RevokesCurrentAndRegistersReplacement()
    {
        var service = NewService();
        var user = NewUser();
        var oldRefreshToken = service.IssueTokens(user).RefreshToken;
        var current = user.FindRefreshToken(service.HashRefreshToken(oldRefreshToken))!;

        var tokens = service.RotateTokens(user, current);

        var replacement = user.FindRefreshToken(service.HashRefreshToken(tokens.RefreshToken))!;
        Assert.Equal(replacement.Id, current.ReplacedByTokenId);
        Assert.Equal(Now.UtcDateTime, current.RevokedAt);
        Assert.Equal(20 * 60, tokens.ExpiresIn);
    }

    private static User NewUser() => User.CreateCommon("Ana", "ana@mail.com", "hash");
}
