using System.Collections.Generic;

namespace CLup.Application.Shared;

public sealed class ProblemDetails
{
    public string Type { get; private set; } = "https://datatracker.ietf.org/doc/html/rfc7231#sections-6.5.1";

    public string Title { get; private set; }

    public HttpCode StatusCode { get; private set; }

    public IDictionary<string, List<string>> Errors { get; private set; }

    public ProblemDetails(HttpCode statusCode, IDictionary<string, List<string>> errors, string title = "Invalid Request")
    {
        StatusCode = statusCode;
        Errors = errors;
        Title = title;
    }
}
