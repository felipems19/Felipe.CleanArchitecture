using Asp.Versioning;
using Felipe.CleanArchitecture.Application.Models.Requests;
using Felipe.CleanArchitecture.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Felipe.CleanArchitecture.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/trucks")]
public class TrucksController() : BaseAppController
{
    [HttpPost]
    public async Task<IActionResult> RegisterTruck([FromBody] RegisterTruckRequest request, [FromServices] IAddTruckUseCase registerTruckUseCase)
    {
        await registerTruckUseCase.ExecuteAsync(Guid.NewGuid(), request.LicensePlate, request.Model);
        return Ok(new { Message = "Caminhão registrado com sucesso!" });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] IGetAllTrucksUseCase useCase)
    {
        var trucks = await useCase.ExecuteAsync();
        return Ok(trucks);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, [FromServices] IGetTruckByIdUseCase useCase)
    {
        var truck = await useCase.ExecuteAsync(id);
        if (truck is null) return NotFound();
        return Ok(truck);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTruck(Guid id, [FromBody] RegisterTruckRequest request, [FromServices] IUpdateTruckUseCase useCase)
    {
        await useCase.ExecuteAsync(id, request.LicensePlate, request.Model);
        return Ok(new { Message = "Caminhão atualizado com sucesso!" });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTruck(Guid id, [FromServices] IDeleteTruckUseCase useCase)
    {
        await useCase.ExecuteAsync(id);
        return Ok(new { Message = "Caminhão excluído com sucesso!" });
    }
}
