using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using Felipe.CleanArchitecture.Domain.Entities;
using Felipe.CleanArchitecture.Domain.SeedWork;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Create;

public class CreateTruckCommandHandler(IRepository<Truck> repository)
    : IRequestHandler<CreateTruckCommand, Result<TruckOperationDto>>
{
    public async Task<Result<TruckOperationDto>> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
    {
        var truck = new Truck(request.LicensePlate, request.Model, request.LastMaintenanceDate);

        await repository.AddAsync(truck, cancellationToken);
        await repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new TruckOperationDto("Caminhão registrado com sucesso."));
    }
}
