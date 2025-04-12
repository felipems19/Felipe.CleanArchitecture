using Felipe.CleanArchitecture.Application.Models.Responses;
using Felipe.CleanArchitecture.Domain.Entities;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using FluentResults;

namespace Felipe.CleanArchitecture.Application.UseCases;

public interface IAddTruckUseCase
{
    Task<Result<DefaultTruckResponse>> ExecuteAsync(Guid id, string licensePlate, string model);
}

public class AddTruckUseCase(ITruckRepository repository) : IAddTruckUseCase
{
    public async Task<Result<DefaultTruckResponse>> ExecuteAsync(Guid id, string licensePlate, string model)
    {
        var truck = new Truck(id, licensePlate, model);
        await repository.AddAsync(truck);

        return Result.Ok(new DefaultTruckResponse("Caminhão registrado com sucesso."));
    }
}
