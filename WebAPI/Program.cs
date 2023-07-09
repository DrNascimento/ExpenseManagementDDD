using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

var host = WebApplication.CreateBuilder(args);

        // Obtenha uma instância do DbContextOptions a partir das configurações
        var options = new DbContextOptionsBuilder<ExpanseManagementContext>()
            .UseSqlServer(host.Configuration.GetConnectionString("DefaultConnection"))
            .Options;

        var app = host.Build();
        // Execute as migrações do banco de dados
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<ExpanseManagementContext>();
            dbContext.Database.Migrate();
        }

        app.Run();
