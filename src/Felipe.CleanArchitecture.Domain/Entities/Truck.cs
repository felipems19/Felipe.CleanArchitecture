using Felipe.CleanArchitecture.Domain.Events;
using Felipe.CleanArchitecture.Domain.SeedWork;

namespace Felipe.CleanArchitecture.Domain.Entities;

public class Truck : BaseAuditableEntity
{
    public string LicensePlate { get; private set; }
    public string Model { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    public Truck(Guid id, string licensePlate, string model)
    {
        Id = id;
        LicensePlate = licensePlate;
        Model = model;
        RegisteredAt = DateTime.UtcNow;

        // Emitir evento de registro
        AddDomainEvent(new TruckRegisteredEvent
        {
            TruckId = id,
            LicensePlate = licensePlate,
            Model = model,
            RegisteredAt = RegisteredAt
        });
    }

    public void UpdateInfo(string licensePlate, string model)
    {
        LicensePlate = licensePlate;
        Model = model;

        // Emitir evento de atualização
        AddDomainEvent(new TruckUpdatedEvent
        {
            TruckId = Id,
            LicensePlate = licensePlate,
            Model = model,
            UpdatedAt = DateTime.UtcNow
        });
    }

    public void Delete()
    {
        // Emitir evento de exclusão
        AddDomainEvent(new TruckDeletedEvent
        {
            TruckId = Id,
            DeletedAt = DateTime.UtcNow
        });
    }
}
