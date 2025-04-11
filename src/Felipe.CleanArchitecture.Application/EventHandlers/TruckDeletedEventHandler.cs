using Felipe.CleanArchitecture.Application.Interfaces;
using Felipe.CleanArchitecture.Domain.Events;

namespace Felipe.CleanArchitecture.Application.EventHandlers;

public class TruckDeletedEventHandler : IEventHandler<TruckDeletedEvent>
{
    public Task Handle(TruckDeletedEvent domainEvent)
    {
        Console.WriteLine($"Caminhão excluído: {domainEvent.TruckId}");
        return Task.CompletedTask;
    }
}
