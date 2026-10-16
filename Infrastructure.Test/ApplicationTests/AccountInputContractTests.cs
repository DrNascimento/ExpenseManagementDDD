using System.Reflection;
using System.Text.Json.Serialization;
using Application.DTO.Input.Account;

namespace Infrastructure.Test.ApplicationTests;

public class AccountInputContractTests
{
    private static IEnumerable<string> JsonNames<T>() =>
        typeof(T).GetProperties()
            .Select(p => p.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? p.Name);

    [Fact]
    public void AC2_CreateAccountInput_HasNoTypeUserField()
    {
        Assert.DoesNotContain("type_user", JsonNames<CreateAccountInput>());
        Assert.Equal(
            new[] { "confirm_password", "email", "name", "password" },
            JsonNames<CreateAccountInput>().OrderBy(n => n));
    }

    [Fact]
    public void AC6_LoginInput_HasNoIsRememberMeField()
    {
        Assert.DoesNotContain("is_remember_me", JsonNames<LoginInput>());
        Assert.Equal(new[] { "email", "password" }, JsonNames<LoginInput>().OrderBy(n => n));
    }
}
