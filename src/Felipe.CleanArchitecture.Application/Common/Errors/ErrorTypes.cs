using System.Text.RegularExpressions;
using FluentResults;
using FluentValidation.Results;

namespace Felipe.CleanArchitecture.Application.Common.Errors;

// Error types, Need to update BaseAppController if new error type is added

public class ConflictError(string message) : Error(message) { }

public class BadRequestError(string message) : Error(message) { }

public class ForbiddenAccessError(string message) : Error(message) { }

public class NotFoundError(string message) : Error(message) { }

public class InvalidOperationError(string message) : Error(message) { }

public class UnauthorizedAccessError(string message) : Error(message) { }

public class DeniedAccessError(string message) : Error(message) { }

public class DeniedVpnAccessError(string message) : Error(message) { }

public class BlockedUserError(string message) : Error(message) { }

public class IncompleteProfileError(string message) : Error(message) { }

public class OTPRequiredError(string message) : Error(message) { }

public class InvalidOtpError(string message) : Error(message) { }

public class ExpiredOtpError(string message) : Error(message) { }

public class ExceededAttemptsOtpError(string message) : Error(message) { }

public class ResendOtpExceededError(string message) : Error(message) { }

public class ResendOtpTemporaryBlockedError(string message) : Error(message) { }

public class ExpiredTokenError(string message) : Error(message) { }

public class ValidationError : Error

{

    public ValidationError(string message) : base(message)

    {

    }

    public ValidationError(string message, IList<ValidationFailure> failures) : base(message)

    {

        var aggregatedErrors = new Dictionary<string, List<string>>();

        foreach (var failure in failures)

        {

            // Check if the property name contains an indexed collection item

            var propertyName = failure.PropertyName;

            var match = Regex.Match(propertyName, @"^(.+)\[\d+\](\..+)?$");

            if (match.Success)

            {

                // Use the collection name as the key

                propertyName = match.Groups[1].Value;

            }

            if (!aggregatedErrors.ContainsKey(propertyName))

            {

                aggregatedErrors[propertyName] = new List<string>();

            }

            aggregatedErrors[propertyName].Add(failure.ErrorMessage);

        }

        foreach (var error in aggregatedErrors)

        {

            Metadata.Add(error.Key, error.Value);

        }

    }

}
