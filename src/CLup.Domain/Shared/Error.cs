namespace CLup.Domain.Shared;

public class Error
{
    public string Code { get; init; }
    public string Message { get; init; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public IDictionary<string, List<string>> ToErrorDictionary() =>
        new Dictionary<string, List<string>>() { { Code, new List<string>() { Message } } };
}
