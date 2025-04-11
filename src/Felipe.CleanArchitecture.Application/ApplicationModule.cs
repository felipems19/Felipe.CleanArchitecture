using Felipe.CleanArchitecture.Application.EventDispatching;
using Felipe.CleanArchitecture.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Felipe.CleanArchitecture.Application;

public static class ApplicationModule
{
    public static void AddApplicationModule(this IServiceCollection services)
    {
        services.AddScoped<IRegisterTruckUseCase, RegisterTruckUseCase>();
        services.AddScoped<IEventDispatcher, EventDispatcher>();
    }
}
