using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Felipe.CleanArchitecture.Api.Infrastructure.ProblemDetails;

/// <summary>
/// A custom implementation of ProblemDetails that includes validation errors.
/// </summary>
public class CustomValidationProblemDetails : ValidationProblemDetails
{
    public string? ErrorReference { get; set; }

    [JsonPropertyName("errors")]
    public new IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();

    [JsonIgnore]
    public new IDictionary<string, object>? Extensions { get; set; }

    public CustomValidationProblemDetails()
    {
    }

    public CustomValidationProblemDetails(ModelStateDictionary modelState)
    {
        foreach (var keyModelStatePair in modelState)
        {
            var errors = keyModelStatePair.Value.Errors;
            var field = keyModelStatePair.Key;

            if (errors.Count > 0)
            {
                Errors[field] = errors.Select(e => e.ErrorMessage).ToArray();
            }
        }
    }
}
