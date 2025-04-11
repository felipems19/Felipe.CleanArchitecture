namespace Felipe.CleanArchitecture.Application.Common.Exceptions;

/// <summary>
/// Exception type for application exceptions
/// </summary>
public class ConfigurationException : Exception
{
    public ConfigurationException()
    { }

    public ConfigurationException(string message)
        : base(message)
    { }

    public ConfigurationException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
