using Felipe.CleanArchitecture.Domain.Events;
using Felipe.CleanArchitecture.Domain.SeedWork;

namespace Felipe.CleanArchitecture.Domain.Entities;

public class Truck : BaseAuditableEntity
{
    public string LicensePlate { get; private set; }
    public string Model { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public DateTime? LastMaintenanceDate { get; private set; }

    // Construtor principal
    public Truck(string licensePlate, string model, DateTime? lastMaintenanceDate = null)
    {
        LicensePlate = licensePlate;
        Model = model;
        RegisteredAt = DateTime.UtcNow;
        LastMaintenanceDate = lastMaintenanceDate;

        AddDomainEvent(new TruckRegisteredEvent
        {
            TruckId = Id,
            LicensePlate = licensePlate,
            Model = model,
            RegisteredAt = RegisteredAt
        });
    }

    public void UpdateInfo(string licensePlate, string model)
    {
        LicensePlate = licensePlate;
        Model = model;

        AddDomainEvent(new TruckUpdatedEvent
        {
            TruckId = Id,
            LicensePlate = licensePlate,
            Model = model,
            UpdatedAt = DateTime.UtcNow
        });
    }

    public void UpdateMaintenanceDate(DateTime? maintenanceDate)
    {
        LastMaintenanceDate = maintenanceDate;

        // AddDomainEvent(new TruckMaintenanceUpdatedEvent { ... });
    }

    public void Delete()
    {
        AddDomainEvent(new TruckDeletedEvent
        {
            TruckId = Id,
            DeletedAt = DateTime.UtcNow
        });
    }

    public bool IsMaintenanceOverdue()
    {
        var now = DateTime.UtcNow;

        if (LastMaintenanceDate.HasValue)
        {
            return (now - LastMaintenanceDate.Value).TotalDays > 180;
        }

        // Sem manutenção registrada, considera vencido se já se passaram 180 dias desde o registro
        return (now - RegisteredAt).TotalDays > 180;
    }
}
