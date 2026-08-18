using Application.DTO.Input.Account;
using Application.DTO.Input.Category;
using Application.DTO.Input.Expense;
using Application.DTO.Input.ExpenseInstallment;
using Application.DTO.Input.ExpenseType;
using Application.DTO.Input.User;
using Application.Handlers.Account;
using Application.Handlers.Category;
using Application.Handlers.Expense;
using Application.Handlers.ExpenseInstallment;
using Application.Handlers.ExpenseType;
using Application.Handlers.User;
using Application.Interfaces;
using Application.RequestValidation.Account;
using Application.RequestValidation.Category;
using Application.RequestValidation.Expense;
using Application.RequestValidation.ExpenseInstallment;
using Application.RequestValidation.ExpenseType;
using Application.RequestValidation.User;
using Application.Services;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using FluentValidation;
using Infrastructure.Data.Repository;
using Infrastructure.Data.Repository.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CrossCutting;

public static class ExpenseManagementDI
{
    public static void RegisterServices(IServiceCollection services)
    {
        // Infrastructure
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IExpenseInstallmentRepository, ExpenseInstallmentRepository>();
        services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();

        // Services still required
        services.AddScoped<ITokenAppService, TokenAppService>();

        // Validators
        services.AddScoped<IValidator<CreateAccountInput>, CreateAccountValidation>();
        services.AddScoped<IValidator<CreateCategoryInput>, CreateCategoryValidation>();
        services.AddScoped<IValidator<UpdateCategoryInput>, UpdateCategoryValidation>();
        services.AddScoped<IValidator<CreateExpenseInput>, CreateExpenseValidation>();
        services.AddScoped<IValidator<UpdateExpenseInput>, UpdateExpenseValidation>();
        services.AddScoped<IValidator<UpdateExpenseInstallmentInput>, UpdateExpenseInstallmentValidation>();
        services.AddScoped<IValidator<CreateExpenseTypeInput>, CreateExpenseTypeValidation>();
        services.AddScoped<IValidator<UpdateUserInput>, UpdateUserValidation>();

        // Account handlers
        services.AddScoped<CreateAccountHandler>();
        services.AddScoped<LoginAccountHandler>();

        // User handlers
        services.AddScoped<GetUserByIdHandler>();
        services.AddScoped<GetAllUsersHandler>();
        services.AddScoped<UpdateUserHandler>();

        // Category handlers
        services.AddScoped<GetCategoryHandler>();
        services.AddScoped<GetAllCategoriesHandler>();
        services.AddScoped<CreateCategoryByUserHandler>();
        services.AddScoped<CreateCategoryUniversalHandler>();
        services.AddScoped<UpdateCategoryHandler>();
        services.AddScoped<DeleteCategoryHandler>();
        services.AddScoped<GetCategorySummaryHandler>();

        // Expense handlers
        services.AddScoped<CreateExpenseHandler>();
        services.AddScoped<GetExpenseHandler>();
        services.AddScoped<GetAllExpensesHandler>();
        services.AddScoped<UpdateExpenseHandler>();
        services.AddScoped<DeleteExpenseHandler>();

        // Expense installment handlers
        services.AddScoped<GetExpenseInstallmentHandler>();
        services.AddScoped<GetExpenseInstallmentsByDateHandler>();
        services.AddScoped<UpdateExpenseInstallmentHandler>();
        services.AddScoped<TogglePaidExpenseInstallmentHandler>();
        services.AddScoped<DeleteExpenseInstallmentHandler>();

        // Expense type handlers
        services.AddScoped<GetExpenseTypeByIdHandler>();
        services.AddScoped<GetAllExpenseTypesHandler>();
        services.AddScoped<CreateExpenseTypeHandler>();
    }
}
