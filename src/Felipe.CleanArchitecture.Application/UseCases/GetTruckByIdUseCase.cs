using Felipe.CleanArchitecture.Domain.Entities;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;

namespace Felipe.CleanArchitecture.Application.UseCases;

public interface IGetTruckByIdUseCase
{
    Task<Truck?> ExecuteAsync(Guid id);
}

public class GetTruckByIdUseCase(ITruckRepository repository) : IGetTruckByIdUseCase
{
    public async Task<Truck?> ExecuteAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }
}
