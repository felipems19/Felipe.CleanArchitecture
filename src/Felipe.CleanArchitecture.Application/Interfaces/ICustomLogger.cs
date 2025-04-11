using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;

namespace Felipe.CleanArchitecture.Application.Interfaces;

public interface ICustomLogger<T> where T : class
{
    TelemetryClient TelemetryClient { get; }
    ILogger<T> Logger { get; }
}
