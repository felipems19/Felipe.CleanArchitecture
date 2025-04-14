using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Create;

public record CreateTruckCommand(
    string LicensePlate,
    string Model,
    DateTime? LastMaintenanceDate
) : IRequest<Result<TruckOperationDto>>;
