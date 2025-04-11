using System.Text.Json.Serialization;
using AspNetCoreProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace Felipe.CleanArchitecture.Api.Infrastructure.ProblemDetails;

/// <summary>
/// Custom problem details.
/// </summary>
public class CustomProblemDetails : AspNetCoreProblemDetails
{
    public string? ErrorReference { get; set; }

    [JsonIgnore]
    public new IDictionary<string, object>? Extensions { get; set; }
}
