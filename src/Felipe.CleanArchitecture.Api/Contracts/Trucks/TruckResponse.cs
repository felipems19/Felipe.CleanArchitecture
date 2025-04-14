namespace Felipe.CleanArchitecture.Api.Contracts.Trucks;
public record TruckResponse
(
    string LicensePlate,
    string Model,
    string RegisteredAt,
    string MaintenanceStatus // "OK" ou "Vencida"
);
