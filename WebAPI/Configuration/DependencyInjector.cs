using Domain.Commands.UserCommands;
using Domain.Validations;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.Identity;
using MediatR;

namespace WebAPI.Configuration
{
    public static class DependencyInjector
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
            services.AddScoped<IUserContext, UserContext>();

            ExpanseManagementDI.RegisterServices(services);
        }

       
        public static void RegisterBehaviors(this Microsoft.Extensions.DependencyInjection.MediatRServiceConfiguration configuration)
        {
            configuration.AddBehavior<IPipelineBehavior<CreateUserCommand, int>, ValidationBehavior<CreateUserCommand, int>>();
        }

    }
}
