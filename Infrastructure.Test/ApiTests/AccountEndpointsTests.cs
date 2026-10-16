using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Enums;
using Infrastructure.Test.Support;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebAPI.Controller;

namespace Infrastructure.Test.ApiTests;

public class AccountEndpointsTests : IDisposable
{
    private const string Password = "secret123";

    private readonly AuthApiFactory _factory = new();
    private readonly HttpClient _client;

    public AccountEndpointsTests()
    {
        _client = _factory.CreateClient();
        _factory.CreateDatabase();
    }

    public void Dispose()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    private record TokenPair(
        [property: JsonPropertyName("access_token")] string AccessToken,
        [property: JsonPropertyName("refresh_token")] string RefreshToken,
        [property: JsonPropertyName("expires_in")] int ExpiresIn);

    private Task<HttpResponseMessage> PostSignUp(object body) => _client.PostAsJsonAsync("api/account/sign-up", body);

    private Task<HttpResponseMessage> PostLogin(string email, string password) =>
        _client.PostAsJsonAsync("api/account/login", new { email, password });

    private Task<HttpResponseMessage> PostRefresh(string refreshToken) =>
        _client.PostAsJsonAsync("api/account/refresh", new { refresh_token = refreshToken });

    private Task<HttpResponseMessage> PostLogout(string refreshToken) =>
        _client.PostAsJsonAsync("api/account/logout", new { refresh_token = refreshToken });

    private static object SignUpBody(string email = "ana@mail.com") => new
    {
        name = "Ana",
        email,
        password = Password,
        confirm_password = Password
    };

    private async Task<TokenPair> SignUp(string email = "ana@mail.com")
    {
        var response = await PostSignUp(SignUpBody(email));
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<TokenPair>())!;
    }

    private static string Sha256(string value) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));

    private int UserCount() => _factory.WithContext(c => c.Users.Count());

    private int RefreshTokenCount() => _factory.WithContext(c => c.RefreshTokens.Count());

    private Domain.Entities.RefreshToken StoredToken(string rawToken) =>
        _factory.WithContext(c => c.RefreshTokens.AsNoTracking().Single(t => t.TokenHash == Sha256(rawToken)));

    private static async Task AssertErrorMessage(HttpResponseMessage response, string message)
    {
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.True(json.RootElement.GetProperty("error").GetBoolean());
        Assert.Contains(
            json.RootElement.GetProperty("errors").EnumerateArray(),
            e => e.GetProperty("message").GetString() == message);
    }

    // Sign-up

    [Fact]
    public async Task AC1_SignUp_CreatesCommonUser_EvenWhenTypeUserIsSent()
    {
        var response = await PostSignUp(new
        {
            name = "Ana",
            email = "ana@mail.com",
            password = Password,
            confirm_password = Password,
            type_user = (int)UserTypeEnum.Admin
        });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(UserTypeEnum.Common, _factory.WithContext(c => c.Users.Single().UserTypeEnum));
    }

    [Fact]
    public async Task AC3_SignUp_Returns201WithTokenPair()
    {
        var response = await PostSignUp(SignUpBody());

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(
            new[] { "access_token", "expires_in", "refresh_token" },
            json.RootElement.EnumerateObject().Select(p => p.Name).OrderBy(n => n));
        Assert.False(string.IsNullOrWhiteSpace(json.RootElement.GetProperty("access_token").GetString()));
        Assert.False(string.IsNullOrWhiteSpace(json.RootElement.GetProperty("refresh_token").GetString()));
        Assert.Equal(1200, json.RootElement.GetProperty("expires_in").GetInt32());
    }

    [Fact]
    public async Task AC4_SignUp_Returns400AndCreatesNothing_WhenEmailAlreadyRegistered()
    {
        await SignUp();

        var response = await PostSignUp(SignUpBody());

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        await AssertErrorMessage(response, "E-mail already registered.");
        Assert.Equal(1, UserCount());
        Assert.Equal(1, RefreshTokenCount());
    }

    [Fact]
    public async Task AC4_SignUp_Returns400AndCreatesNothing_WhenFieldIsInvalid()
    {
        var response = await PostSignUp(new
        {
            name = "Ana",
            email = "ana@mail.com",
            password = Password,
            confirm_password = "different"
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        await AssertErrorMessage(response, "Password confirmation does not match.");
        Assert.Equal(0, UserCount());
        Assert.Equal(0, RefreshTokenCount());
    }

    // Login

    [Fact]
    public async Task AC5_Login_Returns200WithTokenPair()
    {
        await SignUp();

        var response = await PostLogin("ana@mail.com", Password);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var pair = await response.Content.ReadFromJsonAsync<TokenPair>();
        Assert.False(string.IsNullOrWhiteSpace(pair!.AccessToken));
        Assert.False(string.IsNullOrWhiteSpace(pair.RefreshToken));
        Assert.Equal(1200, pair.ExpiresIn);
        Assert.True(StoredToken(pair.RefreshToken).IsActive(_factory.Clock.GetUtcNow().UtcDateTime));
    }

    [Theory]
    [InlineData("ana@mail.com", "wrong-password")]
    [InlineData("nobody@mail.com", Password)]
    public async Task AC7_Login_Returns401AndIssuesNoToken_WhenCredentialsAreInvalid(string email, string password)
    {
        await SignUp();

        var response = await PostLogin(email, password);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        await AssertErrorMessage(response, "Invalid e-mail or password. Please try again.");
        Assert.Equal(1, RefreshTokenCount());
    }

    // Tokens

    [Fact]
    public async Task AC9_ProtectedEndpoint_Returns401_WhenAccessTokenExpired()
    {
        _factory.Clock.Set(DateTimeOffset.UtcNow.AddMinutes(-21));
        var pair = await SignUp();

        var request = new HttpRequestMessage(HttpMethod.Get, "api/users/profile");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", pair.AccessToken);
        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task AC9_ProtectedEndpoint_Returns200_WhenAccessTokenIsStillValid()
    {
        _factory.Clock.Set(DateTimeOffset.UtcNow.AddMinutes(-19));
        var pair = await SignUp();

        var request = new HttpRequestMessage(HttpMethod.Get, "api/users/profile");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", pair.AccessToken);
        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task AC10_RefreshToken_IsPersistedOnlyAsSha256Hash()
    {
        var pair = await SignUp();

        var stored = _factory.WithContext(c => c.RefreshTokens.AsNoTracking().Single());

        Assert.Equal(Sha256(pair.RefreshToken), stored.TokenHash);
        Assert.False(_factory.WithContext(c => c.RefreshTokens.Any(t => t.TokenHash == pair.RefreshToken)));
    }

    // Refresh

    [Fact]
    public async Task AC11_Refresh_RevokesTokenAndReturnsNewPair()
    {
        var pair = await SignUp();
        _factory.Clock.Advance(TimeSpan.FromMinutes(25));

        var response = await PostRefresh(pair.RefreshToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var newPair = (await response.Content.ReadFromJsonAsync<TokenPair>())!;
        Assert.NotEqual(pair.RefreshToken, newPair.RefreshToken);
        Assert.False(string.IsNullOrWhiteSpace(newPair.AccessToken));
        Assert.Equal(1200, newPair.ExpiresIn);

        var old = StoredToken(pair.RefreshToken);
        var replacement = StoredToken(newPair.RefreshToken);
        Assert.Equal(_factory.Clock.GetUtcNow().UtcDateTime, old.RevokedAt);
        Assert.Equal(replacement.Id, old.ReplacedByTokenId);
        Assert.Null(replacement.RevokedAt);
    }

    [Fact]
    public async Task AC12_Refresh_Returns401_WhenTokenIsUnknown()
    {
        await SignUp();

        var response = await PostRefresh("not-a-real-token");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.Equal(1, RefreshTokenCount());
    }

    [Fact]
    public async Task AC12_Refresh_Returns401_WhenTokenIsExpired()
    {
        var pair = await SignUp();
        _factory.Clock.Advance(TimeSpan.FromDays(30));

        var response = await PostRefresh(pair.RefreshToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.Equal(1, RefreshTokenCount());
    }

    [Fact]
    public async Task AC13_Refresh_RevokesAllActiveTokens_WhenRevokedTokenIsReused()
    {
        var pair = await SignUp();
        var otherDevice = (await (await PostLogin("ana@mail.com", Password)).Content.ReadFromJsonAsync<TokenPair>())!;
        var rotated = (await (await PostRefresh(pair.RefreshToken)).Content.ReadFromJsonAsync<TokenPair>())!;

        var reuse = await PostRefresh(pair.RefreshToken);

        Assert.Equal(HttpStatusCode.Unauthorized, reuse.StatusCode);
        Assert.NotNull(StoredToken(rotated.RefreshToken).RevokedAt);
        Assert.NotNull(StoredToken(otherDevice.RefreshToken).RevokedAt);
        Assert.Equal(HttpStatusCode.Unauthorized, (await PostRefresh(rotated.RefreshToken)).StatusCode);
    }

    [Fact]
    public void AC14_RefreshEndpoint_AllowsAnonymous()
    {
        var method = typeof(AccountController).GetMethod(nameof(AccountController.Refresh))!;

        Assert.NotNull(method.GetCustomAttribute<AllowAnonymousAttribute>());
    }

    [Fact]
    public async Task AC14_Refresh_WorksWithoutAccessToken()
    {
        var pair = await SignUp();
        _client.DefaultRequestHeaders.Authorization = null;

        var response = await PostRefresh(pair.RefreshToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // Logout

    [Fact]
    public async Task AC15_Logout_RevokesTokenAndReturns204()
    {
        var pair = await SignUp();
        _factory.Clock.Advance(TimeSpan.FromMinutes(1));

        var response = await PostLogout(pair.RefreshToken);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(_factory.Clock.GetUtcNow().UtcDateTime, StoredToken(pair.RefreshToken).RevokedAt);
    }

    [Fact]
    public async Task AC16_Logout_Returns204WithoutChanges_WhenTokenUnknown()
    {
        var pair = await SignUp();

        var response = await PostLogout("not-a-real-token");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Null(StoredToken(pair.RefreshToken).RevokedAt);
    }

    [Fact]
    public async Task AC16_Logout_Returns204WithoutChanges_WhenTokenAlreadyRevoked()
    {
        var pair = await SignUp();
        var other = (await (await PostLogin("ana@mail.com", Password)).Content.ReadFromJsonAsync<TokenPair>())!;
        await PostLogout(pair.RefreshToken);
        var revokedAt = StoredToken(pair.RefreshToken).RevokedAt;
        _factory.Clock.Advance(TimeSpan.FromMinutes(5));

        var response = await PostLogout(pair.RefreshToken);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(revokedAt, StoredToken(pair.RefreshToken).RevokedAt);
        Assert.Null(StoredToken(other.RefreshToken).RevokedAt);
    }

    [Fact]
    public async Task AC16_Logout_Returns204WithoutChanges_WhenTokenExpired()
    {
        var pair = await SignUp();
        _factory.Clock.Advance(TimeSpan.FromDays(31));

        var response = await PostLogout(pair.RefreshToken);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Null(StoredToken(pair.RefreshToken).RevokedAt);
    }
}
