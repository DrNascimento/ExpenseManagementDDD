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
using Domain.Validations;
using Domain.Commands.UserCommands;
using Infrastructure.Web;
using MediatR.Pipeline;
using FluentValidation;

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

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()).RegisterBehaviors());


builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.UseMiddleware<ExpanseManagementMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();




