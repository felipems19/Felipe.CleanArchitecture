using Felipe.CleanArchitecture.Application.Interfaces;
using Felipe.CleanArchitecture.Domain.Events.Trucks;

namespace Felipe.CleanArchitecture.Application.EventHandlers;

public class TruckUpdatedEventHandler : IEventHandler<TruckUpdatedEvent>
{
    public Task Handle(TruckUpdatedEvent domainEvent)
    {
        Console.WriteLine($"Caminhão atualizado: {domainEvent.TruckId} - {domainEvent.LicensePlate}");
        return Task.CompletedTask;
    }
}
