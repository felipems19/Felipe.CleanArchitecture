using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Update;

public class UpdateTruckCommandHandler(ITruckRepository repository)
    : IRequestHandler<UpdateTruckCommand, Result<TruckOperationDto>>
{
    public async Task<Result<TruckOperationDto>> Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
    {
        var truck = await repository.GetByIdAsync(request.Id);
        if (truck is null)
            return Result.Fail(new NotFoundError("Caminhão não encontrado."));

        truck.UpdateInfo(request.LicensePlate, request.Model);
        await repository.UpdateAsync(truck);

        return Result.Ok(new TruckOperationDto("Caminhão atualizado com sucesso."));
    }
}
