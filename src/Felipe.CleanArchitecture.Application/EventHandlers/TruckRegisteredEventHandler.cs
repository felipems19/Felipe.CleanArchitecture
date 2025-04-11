using Felipe.CleanArchitecture.Application.Interfaces;
using Felipe.CleanArchitecture.Domain.Events;

namespace Felipe.CleanArchitecture.Application.EventHandlers;

public class TruckRegisteredEventHandler : IEventHandler<TruckRegisteredEvent>
{
    public Task Handle(TruckRegisteredEvent domainEvent)
    {
        Console.WriteLine($"Caminhão registrado: {domainEvent.LicensePlate} - {domainEvent.Model}");
        return Task.CompletedTask;
    }
}
