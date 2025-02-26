using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.Identity;

namespace WebAPI.Configuration;

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

        ExpenseManagementDI.RegisterServices(services);
    }
}
