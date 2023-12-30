namespace CLup.Domain.Shared;

public class Result
{
    public bool Success { get; private set; }

    public Error? Error { get; private set; }

    public bool Failure => !Success;

    public Result(Error? error = null)
    {
        Success = error == null;
        Error = error;
    }
}
