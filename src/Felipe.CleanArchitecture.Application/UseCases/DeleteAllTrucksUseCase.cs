using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.Models.Responses;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using FluentResults;

namespace Felipe.CleanArchitecture.Application.UseCases;

public interface IDeleteAllTrucksUseCase
{
    Task<Result<DefaultTruckResponse>> ExecuteAsync();
}

public class DeleteAllTrucksUseCase(ITruckRepository repository) : IDeleteAllTrucksUseCase
{
    public async Task<Result<DefaultTruckResponse>> ExecuteAsync()
    {
        var trucks = await repository.GetAllAsync();

        if (trucks is null || trucks.Count <= 0)
            return Result.Fail(new NotFoundError("Nenhum caminhão encontrado."));

        await repository.DeleteAllAsync(trucks);

        return Result.Ok(new DefaultTruckResponse("Todos os caminhões foram excluídos com sucesso."));
    }
}
