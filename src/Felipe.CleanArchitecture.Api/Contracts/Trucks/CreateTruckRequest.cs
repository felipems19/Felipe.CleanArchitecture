namespace Felipe.CleanArchitecture.Api.Contracts.Trucks;

public record CreateTruckRequest(
    string LicensePlate,
    string Model,
    DateTime? LastMaintenanceDate,
    bool ConfirmTerms // <- campo exclusivo da API
);
