using Application.Interfaces;
using Application.Services;
using Domain.CommandHandlers.UserCommandHandlers;
using Domain.Commands.UserCommands;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.Data.Repository;
using Infrastructure.Data.Repository.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Configuration
{
    public static class DependecyInjector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            

            DependecyInjector.RegisterServices(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            #region Registering

            #region Account
            // AppService
            services.AddScoped<IAccountAppService, AccountAppService>();
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
            #endregion

            #endregion
        }

    }
}
