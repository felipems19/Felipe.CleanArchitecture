using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using Felipe.CleanArchitecture.Domain.Entities;
using Felipe.CleanArchitecture.Domain.SeedWork;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Update;

public class UpdateTruckCommandHandler(IRepository<Truck> repository)
    : IRequestHandler<UpdateTruckCommand, Result<TruckOperationDto>>
{
    public async Task<Result<TruckOperationDto>> Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
    {
        var truck = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (truck is null)
            return Result.Fail(new NotFoundError("Caminhão não encontrado."));

        truck.UpdateInfo(request.LicensePlate, request.Model);

        await repository.UpdateAsync(truck, cancellationToken);
        await repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new TruckOperationDto("Caminhão atualizado com sucesso."));
    }
}
