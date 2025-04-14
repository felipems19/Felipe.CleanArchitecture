using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Delete;

public class DeleteTruckCommandHandler(ITruckRepository repository)
    : IRequestHandler<DeleteTruckCommand, Result<TruckOperationDto>>
{
    public async Task<Result<TruckOperationDto>> Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
    {
        var truck = await repository.GetByIdAsync(request.Id);
        if (truck is null)
            return Result.Fail(new NotFoundError("Caminhão não encontrado."));

        await repository.DeleteAsync(truck);

        return Result.Ok(new TruckOperationDto("Caminhão excluído com sucesso."));
    }
}
