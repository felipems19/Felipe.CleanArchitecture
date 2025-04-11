using Felipe.CleanArchitecture.Api.Infrastructure.Factories;
using Felipe.CleanArchitecture.Application.Common.Exceptions;
using Felipe.CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Felipe.CleanArchitecture.Api.Filters;

public class GlobalExceptionFilter(CustomProblemDetailsFactory problemDetailsFactory, ICustomLogger<GlobalExceptionFilter> customLogger) : IExceptionFilter
{
    private static readonly Dictionary<Type, int> exceptionStatusCodes = new()
    {
        { typeof(InvalidOperationException), StatusCodes.Status400BadRequest },
        { typeof(UnauthorizedAccessException), StatusCodes.Status401Unauthorized },
        { typeof(ForbiddenAccessException), StatusCodes.Status403Forbidden },
        { typeof(NotFoundException), StatusCodes.Status404NotFound },
        { typeof(ConflictException), StatusCodes.Status409Conflict },
        { typeof(ConfigurationException), StatusCodes.Status500InternalServerError },
        { typeof(HttpRequestException), StatusCodes.Status500InternalServerError }
    };

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var exceptionProperties = new Dictionary<string, string>
        {
            { "ExceptionMessage", exception.Message },
            { "StepDescription", "GlobalExceptionFilter" }
        };

        if (exception.Data.Contains("ResponseContent"))
        {
            exceptionProperties["ResponseContent"] = exception.Data["ResponseContent"] as string ?? "";
        }

        customLogger.TelemetryClient.TrackException(exception, exceptionProperties);


        ProblemDetails problemDetails;
        var httpContext = context.HttpContext;

        if (exception is CustomValidationException customValidationException)
        {
            foreach (var item in customValidationException.Failures)
            {
                context.ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }

            problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                httpContext,
                context.ModelState,
                statusCode: StatusCodes.Status400BadRequest,
                detail: httpContext.TraceIdentifier,
                instance: httpContext.Request.Path);
        }
        else
        {
            var statusCode = exceptionStatusCodes.TryGetValue(exception.GetType(), out var status)
                ? status
            : StatusCodes.Status500InternalServerError;

            problemDetails = problemDetailsFactory.CreateProblemDetails(
                httpContext,
                statusCode: statusCode,
                detail: httpContext.TraceIdentifier,
                instance: httpContext.Request.Path);
        }

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };

        context.ExceptionHandled = true;
    }
}
