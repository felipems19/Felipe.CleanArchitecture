﻿namespace Felipe.CleanArchitecture.Application.Common.Exceptions;

/// <summary>
/// Represents error that occurs if a queried object by a particular key is null (not found).
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the NotFoundException class with a specified message.
    /// </summary>
    /// <param name="message">The exception message to deliver.</param>
    public NotFoundException(string message)
    : base(message)
    { }

    /// <summary>
    /// Initializes a new instance of the NotFoundException class with a specified name of the queried object and its key.
    /// </summary>
    /// <param name="objectName">Name of the queried object.</param>
    /// <param name="key">The value by which the object is queried.</param>
    public NotFoundException(string key, string objectName)
        : base($"Queried object {objectName} was not found, Key: {key}")
    {
    }

    /// <summary>
    /// Initializes a new instance of the NotFoundException class with a specified name of the queried object, its key,
    /// and the exception that is the cause of this exception.
    /// </summary>
    /// <param name="objectName">Name of the queried object.</param>
    /// <param name="key">The value by which the object is queried.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public NotFoundException(string key, string objectName, Exception innerException)
        : base($"Queried object {objectName} was not found, Key: {key}", innerException)
    {
    }
}
