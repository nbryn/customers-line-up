using System.Collections.Generic;

namespace CLup.Domain.Shared;

public class DomainResult
{
    public IList<Error> Errors { get; private set; }

    public bool Success => Errors.Count == 0;

    public bool Failure => Errors.Count > 0;

    protected DomainResult(IList<Error> errors)
    {
        Errors = errors;
    }

    public static DomainResult Ok() => new(new List<Error>());

    public static DomainResult Fail(IList<Error> errors) => new(errors);
}
