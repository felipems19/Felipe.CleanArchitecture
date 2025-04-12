using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.Features.Trucks.Models;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace Felipe.CleanArchitecture.Application.Features.Trucks.Get;

public class GetTruckByIdQueryHandler(ITruckRepository repository)
    : IRequestHandler<GetTruckByIdQuery, Result<TruckResponse>>
{
    public async Task<Result<TruckResponse>> Handle(GetTruckByIdQuery request, CancellationToken cancellationToken)
    {
        var truck = await repository.GetByIdAsync(request.Id);
        if (truck is null)
            return Result.Fail(new NotFoundError("Caminhão não encontrado."));

        return Result.Ok(new TruckResponse(truck.LicensePlate, truck.Model));
    }
}
