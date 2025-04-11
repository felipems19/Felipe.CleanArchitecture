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
    public async Task<IActionResult> RegisterTruck([FromBody] RegisterTruckRequest request, [FromServices] IRegisterTruckUseCase registerTruckUseCase)
    {
        await registerTruckUseCase.ExecuteAsync(Guid.NewGuid(), request.LicensePlate, request.Model);
        return Ok(new { Message = "Caminhão registrado com sucesso!" });
    }
}
