using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.EventDispatching;
using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using Felipe.CleanArchitecture.Domain.Entities;
using Felipe.CleanArchitecture.Domain.SeedWork;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Delete;

public class DeleteTruckCommandHandler(
    IRepository<Truck> repository,
    IEventDispatcher dispatcher)
    : IRequestHandler<DeleteTruckCommand, Result<TruckOperationDto>>
{
    public async Task<Result<TruckOperationDto>> Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
    {
        var truck = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (truck is null)
            return Result.Fail(new NotFoundError("Caminhão não encontrado."));

        truck.Delete();

        await repository.DeleteAsync(truck, cancellationToken);
        await repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        await dispatcher.Dispatch(truck.DomainEvents);

        truck.ClearDomainEvents();

        return Result.Ok(new TruckOperationDto("Caminhão excluído com sucesso."));
    }
}
