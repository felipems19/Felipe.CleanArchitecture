using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;

namespace Felipe.CleanArchitecture.Application.UseCases;

public interface IDeleteAllTrucksUseCase
{
    Task ExecuteAsync();
}

public class DeleteAllTrucksUseCase(ITruckRepository repository) : IDeleteAllTrucksUseCase
{
    public async Task ExecuteAsync()
    {
        await repository.DeleteAllAsync();
    }
}
