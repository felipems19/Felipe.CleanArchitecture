using Asp.Versioning;
using Felipe.CleanArchitecture.Application.Common.Errors;
using Felipe.CleanArchitecture.Application.Models.Requests;
using Felipe.CleanArchitecture.Application.Models.Responses;
using Felipe.CleanArchitecture.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Felipe.CleanArchitecture.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/trucks")]
public class TrucksController() : BaseAppController
{
    [HttpPost]
    [ProducesResponseType(typeof(DefaultTruckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterTruck([FromBody] AddTruckRequest request, [FromServices] IAddTruckUseCase registerTruckUseCase)
    {
        return ProcessResult(await registerTruckUseCase.ExecuteAsync(Guid.NewGuid(), request.LicensePlate, request.Model));
    }

    [HttpGet]
    [ProducesResponseType(typeof(AllTrucksResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllTrucks([FromServices] IGetAllTrucksUseCase useCase)
    {
        return ProcessResult(await useCase.ExecuteAsync());
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TruckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTruckById(Guid id, [FromServices] IGetTruckByIdUseCase useCase)
    {
        return ProcessResult(await useCase.ExecuteAsync(id));
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(DefaultTruckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateTruck(Guid id, [FromBody] AddTruckRequest request, [FromServices] IUpdateTruckUseCase useCase)
    {
        return ProcessResult(await useCase.ExecuteAsync(id, request.LicensePlate, request.Model));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DefaultTruckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTruck(Guid id, [FromServices] IDeleteTruckUseCase useCase)
    {
        return ProcessResult(await useCase.ExecuteAsync(id));
    }

    [HttpDelete]
    [ProducesResponseType(typeof(DefaultTruckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAllTrucks([FromServices] IDeleteAllTrucksUseCase useCase)
    {
        return ProcessResult(await useCase.ExecuteAsync());
    }
}
