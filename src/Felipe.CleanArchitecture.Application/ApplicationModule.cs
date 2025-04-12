using Felipe.CleanArchitecture.Application.EventDispatching;
using Felipe.CleanArchitecture.Application.EventHandlers;
using Felipe.CleanArchitecture.Application.Interfaces;
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
        services.AddScoped<IDeleteAllTrucksUseCase, DeleteAllTrucksUseCase>();
        services.AddScoped<IEventDispatcher, EventDispatcher>();

        services.Scan(scan => scan
            .FromAssemblyOf<TruckRegisteredEventHandler>()
            .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}
