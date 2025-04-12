using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Delete;

public record DeleteAllTrucksCommand() : IRequest<Result<TruckOperationResponse>>;
