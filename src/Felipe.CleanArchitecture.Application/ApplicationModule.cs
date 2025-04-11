using Felipe.CleanArchitecture.Application.EventDispatching;
using Felipe.CleanArchitecture.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Felipe.CleanArchitecture.Application;

public static class ApplicationModule
{
    public static void AddApplicationModule(this IServiceCollection services)
    {
        services.AddScoped<IAddTruckUseCase, AddTruckUseCase>();
        services.AddScoped<IGetAllTrucksUseCase, GetAllTrucksUseCase>();
        services.AddScoped<IGetTruckByIdUseCase, GetTruckByIdUseCase>();
        services.AddScoped<IUpdateTruckUseCase, UpdateTruckUseCase>();
        services.AddScoped<IDeleteTruckUseCase, DeleteTruckUseCase>();
        services.AddScoped<IEventDispatcher, EventDispatcher>();
    }
}
