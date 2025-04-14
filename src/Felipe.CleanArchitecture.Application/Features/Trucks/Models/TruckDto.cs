namespace Felipe.CleanArchitecture.Application.Features.Trucks.Models;

public record TruckDto
(
    string LicensePlate,
    string Model,
    DateTime RegisteredAt,
    bool MaintenanceOverdue
);
