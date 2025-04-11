using Felipe.CleanArchitecture.Domain.SeedWork;

namespace Felipe.CleanArchitecture.Domain.Events;

public class TruckDeletedEvent : BaseEvent
{
    public Guid TruckId { get; set; }
    public DateTime DeletedAt { get; set; }
}
