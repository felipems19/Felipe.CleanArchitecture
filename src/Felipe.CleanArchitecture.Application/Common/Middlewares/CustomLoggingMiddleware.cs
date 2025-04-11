using System.Text;
using System.Text.Json;
using Felipe.CleanArchitecture.Application.Models.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace Felipe.CleanArchitecture.Application.Common.Middlewares;

public class CustomLoggingMiddleware(RequestDelegate next, ILogger<CustomLoggingMiddleware> logger, RecyclableMemoryStreamManager recyclableMemoryStreamManager)
{
    public async Task Invoke(HttpContext context)
    {
        var start = DateTime.UtcNow;

        context.Request.EnableBuffering();
        var originalResponse = context.Response.Body;

        await using var response = recyclableMemoryStreamManager.GetStream();
        context.Response.Body = response;

        try
        {
            await next(context);
        }
        finally
        {
            await AssembleLogData(context, start);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await response.CopyToAsync(originalResponse);
            context.Response.Body = originalResponse;
        }
    }

    private async Task AssembleLogData(HttpContext context, DateTime start)
    {
        var logProperties = new LogProperties
        {
            HttpMethod = context.Request.Method,
            RequestedUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}",
            TimeTaken = DateTime.UtcNow - start,
            ResponseCode = context.Response.StatusCode
        };

        var logLevel = context.Response.StatusCode switch
        {
            >= 400 and < 500 => LogLevel.Warning,
            >= 500 => LogLevel.Error,
            _ => LogLevel.Information
        };

        // We are only logging response details for warnings and errors (Http codes equal to or greater than 400)
        if (logLevel != LogLevel.Information)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();

            var parsedResponseText = ParseResponseText(responseText);
            logProperties.ResponseDetails = parsedResponseText;
        }

        LogOperation(logProperties, logLevel, context);
    }

    private void LogOperation(LogProperties logProperties, LogLevel logLevel, HttpContext context)
    {
        using (logger.BeginScope(logProperties))
        {
            if (context != null &&
            !string.IsNullOrEmpty(context.Request?.Method) &&
            !string.IsNullOrEmpty(context.Request?.Path) &&
            !string.IsNullOrEmpty(context.TraceIdentifier))
            {
                logger.Log(logLevel, "{Method} - {Path} - {TraceIdentifier}", context.Request.Method, context.Request.Path, context.TraceIdentifier);
            }
            else
            {
                logger.Log(logLevel, "HTTP Request Information");
            }
        }
    }

    private static string ParseResponseText(string responseText)
    {
        if (string.IsNullOrEmpty(responseText))
        {
            return string.Empty;
        }

        var jsonDoc = JsonDocument.Parse(responseText);
        var root = jsonDoc.RootElement;

        var sb = new StringBuilder();
        if (root.ValueKind == JsonValueKind.Object)
        {
            foreach (var property in root.EnumerateObject())
            {
                sb.AppendLine($"{property.Name}: {property.Value}");
            }
        }
        else if (root.ValueKind == JsonValueKind.String)
        {
            sb.AppendLine($"detail: {root.GetString()}");
        }

        return sb.ToString();
    }
}
