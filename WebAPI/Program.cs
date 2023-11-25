using Application.AutoMapper;
using Infrastructure.CrossCutting;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ExpenseManagementContext>(options =>
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
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()).RegisterBehaviorsValidators());


builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddIdentitySetup(builder.Configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwaggerUI();
app.UseSwagger(x => x.SerializeAsV2 = true);

app.UseMiddleware<ExpenseManagementMiddleware>();

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