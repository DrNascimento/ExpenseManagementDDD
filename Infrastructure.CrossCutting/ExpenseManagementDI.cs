using Application.Interfaces;
using Application.Services;
using Domain.CommandHandlers.CategoryCommandHandlers;
using Domain.CommandHandlers.ExpenseCommandHandlers;
using Domain.CommandHandlers.ExpenseTypeCommandHandlers;
using Domain.CommandHandlers.UserCommandHandlers;
using Domain.Commands.Category;
using Domain.Commands.ExpenseCommands;
using Domain.Commands.ExpenseTypeCommands;
using Domain.Commands.UserCommands;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using Domain.Validations;
using Domain.Validations.CategoryCommandValidations;
using Domain.Validations.Expense;
using Domain.Validations.ExpenseTypeCommandValidations;
using Domain.Validations.UserCommandValidations;
using FluentValidation;
using Infrastructure.Data.Repository;
using Infrastructure.Data.Repository.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CrossCutting
{
    public static class ExpenseManagementDI
    {

        public static void RegisterServices(IServiceCollection services)
        {
            #region Registering

            #region Account
            // AppService
            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<ITokenAppService, TokenAppService>();
            #endregion

            #region Category
            // AppService
            services.AddScoped<ICategoryAppService, CategoryAppService>();

            // Command
            services.AddScoped<IRequestHandler<CreateCategoryCommand, int>, CreateCategoryCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCategoryCommand, Unit>, UpdateCategoryCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteCategoryCommand, Unit>, DeleteCategoryCommandHandler>();

            // Repository
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>();
            services.AddScoped<IValidator<UpdateCategoryCommand>, UpdateCategoryCommandValidator>();
            services.AddScoped<IValidator<DeleteCategoryCommand>, DeleteCategoryCommandValidator>();
            #endregion

            #region Expense
            // AppService
            services.AddScoped<IExpenseAppService, ExpenseAppService>();

            // Command
            services.AddScoped<IRequestHandler<CreateExpenseCommand, int>, CreateExpenseCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateExpenseCommand, Unit>, UpdateExpenseCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteExpenseCommand, Unit>, DeleteExpenseCommandHandler>();


            // Repository
            services.AddScoped<IExpenseRepository, ExpenseRepository>();

            services.AddScoped<IValidator<CreateExpenseCommand>, CreateExpenseCommandValidator>();
            services.AddScoped<IValidator<UpdateExpenseCommand>, UpdateExpenseCommandValidator>();
            services.AddScoped<IValidator<DeleteExpenseCommand>, DeleteExpenseCommandValidator>();
            #endregion

            #region ExpenseInstallment
            // AppService
            services.AddScoped<IExpenseInstallmentAppService, ExpenseInstallmentAppService>();

            // Command
            //services.AddScoped<IRequestHandler<CreateExpenseCommand, int>, CreateExpenseCommandHandler>();
            //services.AddScoped<IRequestHandler<UpdateExpenseTypeCommand, Unit>, UpdateExpenseTypeCommandHandler>();
            //services.AddScoped<IRequestHandler<DeleteExpenseTypeCommand, Unit>, DeleteExpenseTypeCommandHandler>();


            // Repository
            services.AddScoped<IExpenseInstallmentRepository, ExpenseInstallmentRepository>();

            //services.AddScoped<IValidator<CreateExpenseTypeCommand>, CreateExpenseTypeCommandValidator>();
            //services.AddScoped<IValidator<UpdateExpenseTypeCommand>, UpdateExpenseTypeCommandValidator>();
            //services.AddScoped<IValidator<DeleteExpenseTypeCommand>, DeleteExpenseTypeCommandValidator>();
            #endregion

            #region ExpenseType

            // AppService
            services.AddScoped<IExpenseTypeAppService, ExpenseTypeAppService>();

            // Command
            services.AddScoped<IRequestHandler<CreateExpenseTypeCommand, int>, CreateExpenseTypeCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateExpenseTypeCommand, Unit>, UpdateExpenseTypeCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteExpenseTypeCommand, Unit>, DeleteExpenseTypeCommandHandler>();


            // Repository
            services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();

            services.AddScoped<IValidator<CreateExpenseTypeCommand>, CreateExpenseTypeCommandValidator>();
            services.AddScoped<IValidator<UpdateExpenseTypeCommand>, UpdateExpenseTypeCommandValidator>();
            services.AddScoped<IValidator<DeleteExpenseTypeCommand>, DeleteExpenseTypeCommandValidator>();
            #endregion

            #region UnitOfWork

            // AppService
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repository

            #endregion

            #region User

            // AppService
            services.AddScoped<IUserAppService, UserAppService>();

            // Command
            services.AddScoped<IRequestHandler<CreateUserCommand, int>, CreateUserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, Unit>, UpdateUserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand, Unit>, DeleteUserCommandHandler>();


            // Repository
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
            services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();
            #endregion

            #endregion
        }

        public static void RegisterBehaviorsValidators(this MediatRServiceConfiguration configuration)
        {
            // user
            configuration.AddBehavior<IPipelineBehavior<CreateUserCommand, int>, ValidationBehavior<CreateUserCommand, int>>();
            configuration.AddBehavior<IPipelineBehavior<UpdateUserCommand, Unit>, ValidationBehavior<UpdateUserCommand, Unit>>();
            configuration.AddBehavior<IPipelineBehavior<DeleteUserCommand, Unit>, ValidationBehavior<DeleteUserCommand, Unit>>();

            configuration.AddBehavior<IPipelineBehavior<CreateExpenseTypeCommand, int>, ValidationBehavior<CreateExpenseTypeCommand, int>>();
            configuration.AddBehavior<IPipelineBehavior<UpdateExpenseTypeCommand, Unit>, ValidationBehavior<UpdateExpenseTypeCommand, Unit>>();
            configuration.AddBehavior<IPipelineBehavior<DeleteExpenseTypeCommand, Unit>, ValidationBehavior<DeleteExpenseTypeCommand, Unit>>();

            configuration.AddBehavior<IPipelineBehavior<CreateCategoryCommand, int>, ValidationBehavior<CreateCategoryCommand, int>>();
            configuration.AddBehavior<IPipelineBehavior<UpdateCategoryCommand, Unit>, ValidationBehavior<UpdateCategoryCommand, Unit>>();
            configuration.AddBehavior<IPipelineBehavior<DeleteCategoryCommand, Unit>, ValidationBehavior<DeleteCategoryCommand, Unit>>();

            configuration.AddBehavior<IPipelineBehavior<CreateExpenseCommand, int>, ValidationBehavior<CreateExpenseCommand, int>>();
            configuration.AddBehavior<IPipelineBehavior<UpdateExpenseCommand, Unit>, ValidationBehavior<UpdateExpenseCommand, Unit>>();
            configuration.AddBehavior<IPipelineBehavior<DeleteExpenseCommand, Unit>, ValidationBehavior<DeleteExpenseCommand, Unit>>();
        }

    }
}
