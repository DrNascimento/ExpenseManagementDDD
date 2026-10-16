using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Test.Support;

/// <summary>
/// Hosts the real API in memory with a private in-memory SQLite database and a controllable clock.
/// </summary>
public class AuthApiFactory : WebApplicationFactory<Program>
{
    private readonly SqliteConnection _connection = new("DataSource=:memory:");

    public MutableTimeProvider Clock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureTestServices(services =>
        {
            var contextRegistrations = services
                .Where(d => d.ServiceType == typeof(DbContextOptions<ExpenseManagementContext>)
                    || d.ServiceType == typeof(DbContextOptions)
                    || (d.ServiceType.IsGenericType && d.ServiceType.GenericTypeArguments.Contains(typeof(ExpenseManagementContext))))
                .ToList();

            foreach (var descriptor in contextRegistrations)
                services.Remove(descriptor);

            _connection.Open();
            services.AddDbContext<ExpenseManagementContext>(options => options.UseSqlite(_connection));

            services.AddSingleton<TimeProvider>(Clock);
        });
    }

    public void CreateDatabase()
    {
        using var scope = Services.CreateScope();
        scope.ServiceProvider.GetRequiredService<ExpenseManagementContext>().Database.EnsureCreated();
    }

    public T WithContext<T>(Func<ExpenseManagementContext, T> query)
    {
        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ExpenseManagementContext>();
        return query(context);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _connection.Dispose();
    }
}
