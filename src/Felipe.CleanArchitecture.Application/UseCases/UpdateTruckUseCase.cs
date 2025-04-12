using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.Models.Responses;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using FluentResults;

namespace Felipe.CleanArchitecture.Application.UseCases;

public interface IUpdateTruckUseCase
{
    Task<Result<DefaultTruckResponse>> ExecuteAsync(Guid id, string licensePlate, string model);
}

public class UpdateTruckUseCase(ITruckRepository repository) : IUpdateTruckUseCase
{
    public async Task<Result<DefaultTruckResponse>> ExecuteAsync(Guid id, string licensePlate, string model)
    {
        var truck = await repository.GetByIdAsync(id);

        if (truck == null)
            return Result.Fail(new NotFoundError("Caminhão não encontrado."));

        truck.UpdateInfo(licensePlate, model);
        await repository.UpdateAsync(truck);

        return Result.Ok(new DefaultTruckResponse("Caminhão atualizado com sucesso."));
    }
}
