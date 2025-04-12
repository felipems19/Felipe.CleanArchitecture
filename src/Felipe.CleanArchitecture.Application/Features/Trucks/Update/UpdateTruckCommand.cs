using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Update;

public record UpdateTruckCommand(Guid Id, string LicensePlate, string Model)
    : IRequest<Result<TruckOperationResponse>>;
