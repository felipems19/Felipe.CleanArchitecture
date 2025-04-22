using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.EventDispatching;
using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using Felipe.CleanArchitecture.Domain.Entities;
using Felipe.CleanArchitecture.Domain.SeedWork;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Delete;

public class DeleteAllTrucksCommandHandler(
    IRepository<Truck> repository,
    IEventDispatcher dispatcher)
    : IRequestHandler<DeleteAllTrucksCommand, Result<TruckOperationDto>>
{
    public async Task<Result<TruckOperationDto>> Handle(DeleteAllTrucksCommand request, CancellationToken cancellationToken)
    {
        var trucks = await repository.ListAsync(cancellationToken);

        if (trucks is null || trucks.Count <= 0)
            return Result.Fail(new NotFoundError("Nenhum caminhão encontrado."));

        foreach (var truck in trucks)
        {
            truck.Delete();
        }

        await repository.DeleteRangeAsync(trucks, cancellationToken);
        await repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        await dispatcher.Dispatch(trucks.SelectMany(t => t.DomainEvents));
        foreach (var truck in trucks)
            truck.ClearDomainEvents();

        return Result.Ok(new TruckOperationDto("Todos os caminhões foram excluídos com sucesso."));
    }
}
