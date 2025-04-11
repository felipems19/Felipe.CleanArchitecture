using Felipe.CleanArchitecture.Application.Common.Errors;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Felipe.CleanArchitecture.Api.Controllers;

public class BaseAppController : ControllerBase
{
    protected IActionResult ProcessResult(Result data, bool? hasNoContent = false)
    {
        if (data.IsSuccess)
            return hasNoContent == true ? NoContent() : Ok();

        var error = data.Errors.Single();
        return GetError(error);
    }

    protected IActionResult ProcessResult<T>(Result<T> data, bool? hasNoContent = false)
    {
        if (data.IsSuccess)
            return hasNoContent == true ? NoContent() : Ok(data.Value);

        var error = data.Errors.Single();
        return GetError(error);
    }

    // For situations where it is required to send statuscodes other than 200
    public IActionResult ProcessResult<T>(Result<T> data, int statusCode, bool shouldProvideResponseData)
    {
        if (data.IsSuccess)
        {
            if (shouldProvideResponseData)
                return StatusCode(statusCode, data.Value);

            return StatusCode(statusCode);
        }

        var error = data.Errors.Single();
        return GetError(error);
    }

    private IActionResult GetError(IError error)
    {
        return error switch
        {
            BadRequestError or
            ValidationError => AssembleResponse(BadRequest, error),

            UnauthorizedAccessError => AssembleResponse(Unauthorized, error),

            ForbiddenAccessError => new ForbidResult(),

            NotFoundError => AssembleResponse(NotFound, error),

            ConflictError => AssembleResponse(Conflict, error),

            _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
        };
    }

    private static IActionResult AssembleResponse(Func<ErrorDetails, IActionResult> status, IError error)
    {
        var errorDetails = new ErrorDetails(error.Message, error.GetType().Name, error.Reasons);

        return status(errorDetails);
    }
}


