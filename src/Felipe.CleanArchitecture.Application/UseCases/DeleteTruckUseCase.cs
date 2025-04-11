using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;

namespace Felipe.CleanArchitecture.Application.UseCases;

public interface IDeleteTruckUseCase
{
    Task ExecuteAsync(Guid id);
}

public class DeleteTruckUseCase(ITruckRepository repository) : IDeleteTruckUseCase
{
    public async Task ExecuteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }
}
