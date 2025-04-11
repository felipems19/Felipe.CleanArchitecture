using FluentValidation.Results;

namespace Felipe.CleanArchitecture.Application.Common.Exceptions;

/// <summary>
/// Exception that is thrown when a validation fails.
/// </summary>
public class CustomValidationException(IEnumerable<ValidationFailure> _failures) : Exception
{
    public CustomValidationException() : this([]) { }
    public IEnumerable<ValidationFailure> Failures { get; } = _failures;
}
