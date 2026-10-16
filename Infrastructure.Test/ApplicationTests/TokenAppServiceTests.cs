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
    public void AC8_GenerateAccessToken_ExpiresAfterConfiguredMinutes()
    {
        var service = NewService();
        var user = User.CreateCommon("Ana", "ana@mail.com", "hash");

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(service.GenerateAccessToken(user));

        Assert.Equal(Now.UtcDateTime.AddMinutes(20), jwt.ValidTo);
        Assert.Equal(20 * 60, service.AccessTokenExpiresInSeconds);
    }

    [Fact]
    public void AC8_GenerateAccessToken_UsesConfiguredLifetime()
    {
        var service = NewService(new JwtSettings
        {
            SecretKey = "a-test-secret-key-with-enough-length-for-hmac",
            Issuer = "issuer",
            Audience = "audience",
            AccessTokenExpirationInMinutes = 5
        });

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(
            service.GenerateAccessToken(User.CreateCommon("Ana", "ana@mail.com", "hash")));

        Assert.Equal(Now.UtcDateTime.AddMinutes(5), jwt.ValidTo);
        Assert.Equal(300, service.AccessTokenExpiresInSeconds);
    }

    [Fact]
    public void AC8_GenerateRefreshToken_ExpiresAfterConfiguredDays()
    {
        var refreshToken = NewService().GenerateRefreshToken();

        Assert.Equal(Now.UtcDateTime, refreshToken.CreatedAt);
        Assert.Equal(Now.UtcDateTime.AddDays(30), refreshToken.ExpiresAt);
    }

    [Fact]
    public void AC10_GenerateRefreshToken_Is64RandomBytesBase64UrlEncoded()
    {
        var service = NewService();

        var first = service.GenerateRefreshToken();
        var second = service.GenerateRefreshToken();

        Assert.Equal(64, Base64UrlEncoder.DecodeBytes(first.Token).Length);
        Assert.DoesNotContain('+', first.Token);
        Assert.DoesNotContain('/', first.Token);
        Assert.DoesNotContain('=', first.Token);
        Assert.NotEqual(first.Token, second.Token);
    }

    [Fact]
    public void AC10_HashRefreshToken_IsSha256OfToken()
    {
        var service = NewService();
        var refreshToken = service.GenerateRefreshToken();

        var expected = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken.Token)));

        Assert.Equal(expected, refreshToken.TokenHash);
        Assert.Equal(expected, service.HashRefreshToken(refreshToken.Token));
        Assert.NotEqual(refreshToken.Token, refreshToken.TokenHash);
    }
}
