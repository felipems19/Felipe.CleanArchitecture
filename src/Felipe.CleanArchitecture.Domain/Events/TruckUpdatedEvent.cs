using Felipe.CleanArchitecture.Domain.SeedWork;

namespace Felipe.CleanArchitecture.Domain.Events;

public class TruckUpdatedEvent : BaseEvent
{
    public Guid TruckId { get; set; }
    public string? LicensePlate { get; set; }
    public string? Model { get; set; }
    public DateTime UpdatedAt { get; set; }
}
