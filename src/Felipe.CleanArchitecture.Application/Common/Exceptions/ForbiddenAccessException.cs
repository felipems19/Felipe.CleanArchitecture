namespace Felipe.CleanArchitecture.Application.Common.Exceptions;

/// <summary>
/// Exception that is thrown when a user tries to access a resource that he/she is not allowed to.
/// </summary>
public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException()
    { }

    public ForbiddenAccessException(string message)
        : base(message)
    { }

    public ForbiddenAccessException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
