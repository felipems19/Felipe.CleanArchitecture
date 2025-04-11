using Felipe.CleanArchitecture.Domain.Entities;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;

namespace Felipe.CleanArchitecture.Application.UseCases;

public interface IRegisterTruckUseCase
{
    Task ExecuteAsync(Guid id, string licensePlate, string model);
}

public class RegisterTruckUseCase(ITruckRepository repository) : IRegisterTruckUseCase
{
    public async Task ExecuteAsync(Guid id, string licensePlate, string model)
    {
        var truck = new Truck(id, licensePlate, model);
        await repository.AddAsync(truck);
    }
}
