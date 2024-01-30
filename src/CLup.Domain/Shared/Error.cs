namespace CLup.Domain.Shared;

using System.Collections.Generic;

public class Error
{
    public string Code { get; init; }
    public string Message { get; init; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public IDictionary<string, IList<string>> ToErrorDictionary() =>
        new Dictionary<string, IList<string>>() { { Code, new[] { Message } } };
}
