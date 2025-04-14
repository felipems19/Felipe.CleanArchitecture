using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.List;

public class ListTrucksQueryHandler(ITruckRepository repository)
    : IRequestHandler<ListTrucksQuery, Result<TruckListDto>>
{
    public async Task<Result<TruckListDto>> Handle(ListTrucksQuery request, CancellationToken cancellationToken)
    {
        var allTrucks = await repository.GetAllAsync();

        if (allTrucks == null || allTrucks.Count <= 0)
            return Result.Fail(new NotFoundError("Nenhum caminhão encontrado."));

        var truckDetails = allTrucks
            .Select(t => new TruckDto(
                t.LicensePlate,
                t.Model,
                t.RegisteredAt,
                t.IsMaintenanceOverdue()
            )).ToList();

        return Result.Ok(new TruckListDto(truckDetails));
    }
}
