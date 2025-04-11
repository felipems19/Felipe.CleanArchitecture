namespace Felipe.CleanArchitecture.Application.Common.Exceptions;

/// <summary>
/// Exception type for conflict exceptions
/// </summary>
public class ConflictException : Exception
{
    public ConflictException()
    { }

    public ConflictException(string message)
        : base(message)
    { }

    public ConflictException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
