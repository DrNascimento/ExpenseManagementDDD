using Application.AutoMapper;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using WebAPI.Configuration;
using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ExpanseManagementContext>(options =>
    options.UseSqlite(builder.Configuration.GetPathSQLite()));

builder.Services.AddControllers();

builder.Services.AddMvc()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseJsonPolicy();
    });



builder.Services.AddDependencyInjectionConfiguration();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


builder.Services.AddAutoMapper(typeof(MappingProfile));



var app = builder.Build();

        // Execute as migrações do banco de dados
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<ExpanseManagementContext>();
            dbContext.Database.Migrate();
        }

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();




