using Asp.Versioning;
using Felipe.CleanArchitecture.Api.Contracts.Trucks;
using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.Features.Trucks.Create;
using Felipe.CleanArchitecture.Application.Features.Trucks.Delete;
using Felipe.CleanArchitecture.Application.Features.Trucks.Get;
using Felipe.CleanArchitecture.Application.Features.Trucks.List;
using Felipe.CleanArchitecture.Application.Features.Trucks.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Felipe.CleanArchitecture.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/trucks")]
public class TrucksController() : BaseAppController
{
    [HttpPost]
    [ProducesResponseType(typeof(TruckOperationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterTruck(
        [AsParameters] CreateTruckRequest request,
        [FromServices] IMediator mediator)
    {
        var command = new CreateTruckCommand(
            request.LicensePlate,
            request.Model,
            request.LastMaintenanceDate
        );

        var result = await mediator.Send(command);

        return ProcessResult(result.Map(dto => new TruckOperationResponse(dto.Message)));
    }

    [HttpGet]
    [ProducesResponseType(typeof(TruckListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListTrucks([FromServices] IMediator mediator)
    {
        var query = new ListTrucksQuery();

        var result = await mediator.Send(query);

        return ProcessResult(result.Map(dto =>
            new TruckListResponse(
                dto.Trucks.Select(t => new TruckResponse(
                    LicensePlate: t.LicensePlate,
                    Model: t.Model,
                    RegisteredAt: t.RegisteredAt.ToString("dd/MM/yyyy"),
                    MaintenanceStatus: t.MaintenanceOverdue ? "Vencida" : "OK"
                )).ToList()
            )
        ));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TruckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTruckById(Guid id, [FromServices] IMediator mediator)
    {
        var query = new GetTruckByIdQuery(id);

        var result = await mediator.Send(query);

        return ProcessResult(result.Map(dto =>
            new TruckResponse(
                LicensePlate: dto.LicensePlate,
                Model: dto.Model,
                RegisteredAt: dto.RegisteredAt.ToString("dd/MM/yyyy"),
                MaintenanceStatus: dto.MaintenanceOverdue ? "Vencida" : "OK"
            )
        ));
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(TruckOperationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateTruck(
        Guid id,
        [FromBody] CreateTruckRequest request,
        [FromServices] IMediator mediator)
    {
        var command = new UpdateTruckCommand(id, request.LicensePlate, request.Model);

        var result = await mediator.Send(command);

        return ProcessResult(result.Map(dto => new TruckOperationResponse(dto.Message)));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(TruckOperationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTruck(Guid id, [FromServices] IMediator mediator)
    {
        var command = new DeleteTruckCommand(id);

        var result = await mediator.Send(command);

        return ProcessResult(result.Map(dto => new TruckOperationResponse(dto.Message)));
    }

    [HttpDelete]
    [ProducesResponseType(typeof(TruckOperationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAllTrucks([FromServices] IMediator mediator)
    {
        var command = new DeleteAllTrucksCommand();

        var result = await mediator.Send(command);

        return ProcessResult(result.Map(dto => new TruckOperationResponse(dto.Message)));
    }
}
