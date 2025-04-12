using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Get;

public record GetTruckByIdQuery(Guid Id) : IRequest<Result<TruckResponse>>;
