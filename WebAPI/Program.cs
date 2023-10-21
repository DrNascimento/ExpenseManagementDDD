using Application.AutoMapper;
using Infrastructure.Data.Context;
using Infrastructure.Web;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ExpanseManagementContext>(options =>
    options.UseSqlite(builder.Configuration.GetPathSQLite()));
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();

builder.Services.AddMvc()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseJsonPolicy();
    });



builder.Services.AddDependencyInjectionConfiguration();

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()).RegisterBehaviors());


builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddIdentitySetup(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExpanseManagementMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();