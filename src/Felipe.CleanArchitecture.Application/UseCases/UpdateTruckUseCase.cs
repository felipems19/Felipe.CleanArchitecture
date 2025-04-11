using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;

namespace Felipe.CleanArchitecture.Application.UseCases;

public interface IUpdateTruckUseCase
{
    Task ExecuteAsync(Guid id, string licensePlate, string model);
}

public class UpdateTruckUseCase(ITruckRepository repository) : IUpdateTruckUseCase
{
    public async Task ExecuteAsync(Guid id, string licensePlate, string model)
    {
        var truck = await repository.GetByIdAsync(id);
        if (truck is null) throw new Exception("Truck not found");

        truck.UpdateInfo(licensePlate, model);
        await repository.UpdateAsync(truck);
    }
}
