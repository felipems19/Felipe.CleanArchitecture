using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.Models.Responses;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using FluentResults;

namespace Felipe.CleanArchitecture.Application.UseCases;

public interface IGetTruckByIdUseCase
{
    Task<Result<TruckResponse>> ExecuteAsync(Guid id);
}

public class GetTruckByIdUseCase(ITruckRepository repository) : IGetTruckByIdUseCase
{
    public async Task<Result<TruckResponse>> ExecuteAsync(Guid id)
    {
        var truck = await repository.GetByIdAsync(id);

        if (truck is null)
            return Result.Fail(new NotFoundError("Caminhão não encontrado."));

        return new TruckResponse(LicensePlate: truck.LicensePlate, Model: truck.Model);
    }
}
