using Felipe.CleanArchitecture.Domain.Entities;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;

namespace Felipe.CleanArchitecture.Application.UseCases;

public interface IGetAllTrucksUseCase
{
    Task<IEnumerable<Truck>> ExecuteAsync();
}

public class GetAllTrucksUseCase(ITruckRepository repository) : IGetAllTrucksUseCase
{
    public async Task<IEnumerable<Truck>> ExecuteAsync()
    {
        return await repository.GetAllAsync();
    }
}
