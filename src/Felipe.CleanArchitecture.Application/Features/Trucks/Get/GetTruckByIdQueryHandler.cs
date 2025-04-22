using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using Felipe.CleanArchitecture.Domain.Entities;
using Felipe.CleanArchitecture.Domain.SeedWork;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Get;

public class GetTruckByIdQueryHandler(IRepository<Truck> repository)
    : IRequestHandler<GetTruckByIdQuery, Result<TruckDto>>
{
    public async Task<Result<TruckDto>> Handle(GetTruckByIdQuery request, CancellationToken cancellationToken)
    {
        var truck = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (truck is null)
            return Result.Fail(new NotFoundError("Caminhão não encontrado."));

        var dto = new TruckDto(
            LicensePlate: truck.LicensePlate,
            Model: truck.Model,
            RegisteredAt: truck.RegisteredAt,
            MaintenanceOverdue: truck.IsMaintenanceOverdue()
        );

        return Result.Ok(dto);
    }
}
