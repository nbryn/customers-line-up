namespace CLup.Domain.Shared;

public class DomainResult
{
    public bool Success { get; private set; }

    public Error? Error { get; private set; }

    public bool Failure => !Success;

    protected DomainResult(Error? error = null)
    {
        Success = error == null;
        Error = error;
    }

    public static DomainResult Ok() => new();

    public static DomainResult Fail(Error error) => new(error);
}
