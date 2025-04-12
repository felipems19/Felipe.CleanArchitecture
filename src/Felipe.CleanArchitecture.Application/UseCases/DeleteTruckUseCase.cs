using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.Models.Responses;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using FluentResults;

namespace Felipe.CleanArchitecture.Application.UseCases;

public interface IDeleteTruckUseCase
{
    Task<Result<DefaultTruckResponse>> ExecuteAsync(Guid id);
}

public class DeleteTruckUseCase(ITruckRepository repository) : IDeleteTruckUseCase
{
    public async Task<Result<DefaultTruckResponse>> ExecuteAsync(Guid id)
    {
        var truck = await repository.GetByIdAsync(id);

        if (truck is null)
            return Result.Fail(new NotFoundError("Caminhão não encontrado."));

        await repository.DeleteAsync(truck);

        return Result.Ok(new DefaultTruckResponse("Caminhão excluído com sucesso."));
    }
}
