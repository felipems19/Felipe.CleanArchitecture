using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.List;

public record ListTrucksQuery : IRequest<Result<TruckListDto>>;
