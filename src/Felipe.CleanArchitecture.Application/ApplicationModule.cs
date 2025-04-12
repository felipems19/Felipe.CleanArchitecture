using Felipe.CleanArchitecture.Application.EventDispatching;
using Felipe.CleanArchitecture.Application.EventHandlers;
using Felipe.CleanArchitecture.Application.Features.Trucks.Create;
using Felipe.CleanArchitecture.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Felipe.CleanArchitecture.Application;

public static class ApplicationModule
{
    public static void AddApplicationModule(this IServiceCollection services)
    {
        services.AddScoped<IEventDispatcher, EventDispatcher>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateTruckCommandHandler).Assembly);
        });


        services.Scan(scan => scan
            .FromAssemblyOf<TruckRegisteredEventHandler>()
            .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}
