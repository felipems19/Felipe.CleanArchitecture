using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using Felipe.CleanArchitecture.Domain.Entities;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Create;

public class CreateTruckCommandHandler(ITruckRepository repository)
    : IRequestHandler<CreateTruckCommand, Result<TruckOperationResponse>>
{
    public async Task<Result<TruckOperationResponse>> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
    {
        var truck = new Truck(request.Id, request.LicensePlate, request.Model);
        await repository.AddAsync(truck);

        return Result.Ok(new TruckOperationResponse("Caminhão registrado com sucesso."));
    }
}
