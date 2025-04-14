using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using Felipe.CleanArchitecture.Domain.Entities;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Create;

public class CreateTruckCommandHandler(ITruckRepository repository)
    : IRequestHandler<CreateTruckCommand, Result<TruckOperationDto>>
{
    public async Task<Result<TruckOperationDto>> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
    {
        var truck = new Truck(request.LicensePlate, request.Model, request.LastMaintenanceDate);
        await repository.AddAsync(truck);

        return Result.Ok(new TruckOperationDto("Caminhão registrado com sucesso."));
    }
}
