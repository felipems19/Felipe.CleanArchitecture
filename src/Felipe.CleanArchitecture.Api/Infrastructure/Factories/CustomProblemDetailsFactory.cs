using Felipe.CleanArchitecture.Api.Infrastructure.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AspNetCoreProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace Felipe.CleanArchitecture.Api.Infrastructure.Factories;

/// <summary>
/// Factory responsible for creating <see cref="AspNetCoreProblemDetails"/> objects.
/// </summary>
public class CustomProblemDetailsFactory : ProblemDetailsFactory
{
    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= 400;
        type ??= "https://tools.ietf.org/html/rfc9110#section-15.5.1";
        instance ??= httpContext.Request.Path;

        var problemDetails = new CustomValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Instance = instance,
            Detail = detail
        };

        if (title != null)
        {
            problemDetails.Title = title;
        }

        var traceId = httpContext?.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.ErrorReference = traceId;
        }

        return problemDetails;
    }
    public override AspNetCoreProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        var problemDetails = new CustomProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        var traceId = httpContext?.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.ErrorReference = traceId;
        }

        return problemDetails;
    }
}
