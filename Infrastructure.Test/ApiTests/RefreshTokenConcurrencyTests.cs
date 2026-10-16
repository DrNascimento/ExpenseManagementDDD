using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.Test.Support;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Test.ApiTests;

public class RefreshTokenConcurrencyTests : IDisposable
{
    private readonly AuthApiFactory _factory = new();

    public RefreshTokenConcurrencyTests()
    {
        _factory.CreateClient().Dispose();
        _factory.CreateDatabase();
    }

    public void Dispose() => _factory.Dispose();

    [Fact]
    public async Task AC13_ConcurrentRotationOfSameToken_OnlyFirstCommitSucceeds()
    {
        var now = _factory.Clock.GetUtcNow().UtcDateTime;
        using (var seed = _factory.Services.CreateScope())
        {
            var user = global::Domain.Entities.User.CreateCommon("Ana", "ana@mail.com", "hash");
            user.AddRefreshToken("hash-1", now, now.AddDays(30));
            seed.ServiceProvider.GetRequiredService<IUserRepository>().Add(user);
            await seed.ServiceProvider.GetRequiredService<IUnitOfWork>().CommitAsync();
        }

        using var first = _factory.Services.CreateScope();
        using var second = _factory.Services.CreateScope();
        var firstUser = await first.ServiceProvider.GetRequiredService<IUserRepository>().GetByRefreshTokenHash("hash-1");
        var secondUser = await second.ServiceProvider.GetRequiredService<IUserRepository>().GetByRefreshTokenHash("hash-1");

        firstUser!.RotateRefreshToken(firstUser.FindRefreshToken("hash-1")!, "hash-2", now, now.AddDays(30));
        secondUser!.RotateRefreshToken(secondUser.FindRefreshToken("hash-1")!, "hash-3", now, now.AddDays(30));

        await first.ServiceProvider.GetRequiredService<IUnitOfWork>().CommitAsync();
        await Assert.ThrowsAsync<ConcurrencyConflictException>(() =>
            second.ServiceProvider.GetRequiredService<IUnitOfWork>().CommitAsync());

        Assert.False(_factory.WithContext(c => c.RefreshTokens.Any(t => t.TokenHash == "hash-3")));
    }
}
