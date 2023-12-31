using System.Collections.Generic;

namespace CLup.Application.Shared;

public sealed class ProblemDetails
{
    public string Type { get; } = "https://datatracker.ietf.org/doc/html/rfc7231#sections-6.5.1";

    public string Title { get; }

    public HttpCode StatusCode { get; }

    public IDictionary<string, string> Errors { get; }

    public ProblemDetails(HttpCode statusCode, IDictionary<string, string> errors, string title = "Invalid Request")
    {
        StatusCode = statusCode;
        Errors = errors;
        Title = title;
    }
}
