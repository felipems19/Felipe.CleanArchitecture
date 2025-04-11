using Felipe.CleanArchitecture.Application.Interfaces;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;

namespace Felipe.CleanArchitecture.Infrastructure.Services.Observability;

public class CustomLogger<T>(ILogger<T> logger, TelemetryClient telemetryClient) : ICustomLogger<T> where T : class
{
    private readonly ILogger<T> _logger = logger;
    private readonly TelemetryClient _telemetryClient = telemetryClient;

    public TelemetryClient TelemetryClient { get { return _telemetryClient; } }
    public ILogger<T> Logger { get { return _logger; } }
}
