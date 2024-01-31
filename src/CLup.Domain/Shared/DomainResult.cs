using System.Collections.Generic;

namespace CLup.Domain.Shared;

public class DomainResult
{
    public bool Success { get; private set; }

    public List<Error> Errors { get; private set; }

    public bool Failure => !Success;

    protected DomainResult(List<Error> errors)
    {
        Success = errors.Count == 0;
        Errors = errors;
    }

    public static DomainResult Ok() => new(new List<Error>());

    public static DomainResult Fail(List<Error> errors) => new(errors);
}
