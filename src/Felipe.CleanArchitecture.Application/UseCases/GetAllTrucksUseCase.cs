using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.Models.Responses;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using FluentResults;

namespace Felipe.CleanArchitecture.Application.UseCases;

public interface IGetAllTrucksUseCase
{
    Task<Result<AllTrucksResponse>> ExecuteAsync();
}

public class GetAllTrucksUseCase(ITruckRepository repository) : IGetAllTrucksUseCase
{
    public async Task<Result<AllTrucksResponse>> ExecuteAsync()
    {
        var allTrucks = await repository.GetAllAsync();

        if (allTrucks == null || allTrucks.Count <= 0)
            return Result.Fail(new NotFoundError("Nenhum caminhão encontrado."));

        var truckDetails = allTrucks.Select(truck => new TruckResponse(
            truck.LicensePlate,
            truck.Model
        )).ToList();

        return Result.Ok(new AllTrucksResponse(truckDetails));
    }
}
