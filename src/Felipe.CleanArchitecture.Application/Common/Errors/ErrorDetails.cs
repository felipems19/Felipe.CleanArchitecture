using FluentResults;

namespace Felipe.CleanArchitecture.Application.Common.Errors;

public record ErrorDetails(string detail, string type = "", IEnumerable<IReason>? reasons = null);
