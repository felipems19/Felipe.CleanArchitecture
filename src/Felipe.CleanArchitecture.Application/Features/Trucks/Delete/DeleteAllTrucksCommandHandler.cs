using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Delete;

public class DeleteAllTrucksCommandHandler(ITruckRepository repository)
    : IRequestHandler<DeleteAllTrucksCommand, Result<TruckOperationResponse>>
{
    public async Task<Result<TruckOperationResponse>> Handle(DeleteAllTrucksCommand request, CancellationToken cancellationToken)
    {
        var trucks = await repository.GetAllAsync();

        if (trucks is null || trucks.Count <= 0)
            return Result.Fail(new NotFoundError("Nenhum caminhão encontrado."));

        await repository.DeleteAllAsync(trucks);

        return Result.Ok(new TruckOperationResponse("Todos os caminhões foram excluídos com sucesso."));
    }
}
