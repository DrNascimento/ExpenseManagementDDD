﻿using Application.Interfaces;
using Application.Services;
using Domain.CommandHandlers.ExpanseTypeCommandHandlers;
using Domain.CommandHandlers.UserCommandHandlers;
using Domain.Commands.ExpanseTypeCommands;
using Domain.Commands.UserCommands;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using Domain.Validations;
using Domain.Validations.ExpanseTypeCommandValidations;
using Domain.Validations.UserCommandValidations;
using FluentValidation;
using Infrastructure.Data.Repository;
using Infrastructure.Data.Repository.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CrossCutting
{
    public static class ExpanseManagementDI
    {

        public static void RegisterServices(IServiceCollection services)
        {
            #region Registering

            #region Account
            // AppService
            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<ITokenAppService, TokenAppService>();
            #endregion

            #region ExpanseType

            // AppService
            services.AddScoped<IExpanseTypeAppService, ExpanseTypeAppService>();

            // Command
            services.AddScoped<IRequestHandler<CreateExpanseTypeCommand, int>, CreateExpanseTypeCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateExpanseTypeCommand>, UpdateExpanseTypeCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteExpanseTypeCommand>, DeleteExpanseTypeCommandHandler>();


            // Repository
            services.AddScoped<IExpanseTypeRepository, ExpanseTypeRepository>();

            services.AddScoped<IValidator<CreateExpanseTypeCommand>, CreateExpanseTypeCommandValidator>();
            services.AddScoped<IValidator<UpdateExpanseTypeCommand>, UpdateExpanseTypeCommandValidator>();
            services.AddScoped<IValidator<DeleteExpanseTypeCommand>, DeleteExpanseTypeCommandValidator>();
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
            services.AddScoped<IRequestHandler<UpdateUserCommand>, UpdateUserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand>, DeleteUserCommandHandler>();


            // Repository
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
            #endregion

            #endregion
        }

        public static void RegisterBehaviorsValidators(this MediatRServiceConfiguration configuration)
        {
            configuration.AddBehavior<IPipelineBehavior<CreateUserCommand, int>, ValidationBehavior<CreateUserCommand, int>>();
        }

    }
}
