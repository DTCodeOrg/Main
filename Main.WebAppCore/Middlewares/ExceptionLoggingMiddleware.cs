using Main.Infrastructure.CrosscuttingHelperServices;
using Main.Infrastructure.ICrosscuttingServices;

namespace Main.WebAppCore.Middlewares;

public static class ExceptionLoggingMiddleware
{
    public static IServiceCollection AddExceptionLoggingMiddleware (this IServiceCollection services,
    IConfiguration configuration)
    {

        _ = services.AddScoped<IExceptionLoggingService,ExceptionLoggingService> ();

        return services;
    }
}
