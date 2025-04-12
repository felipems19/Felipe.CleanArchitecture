using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Create;

public record CreateTruckCommand(Guid Id, string LicensePlate, string Model)
    : IRequest<Result<TruckOperationResponse>>;
