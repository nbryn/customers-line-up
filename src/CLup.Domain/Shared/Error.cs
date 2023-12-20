namespace CLup.Domain.Shared;

using System.Collections.Generic;

public class Error
{
    public string Code { get; set; }
    public string Message { get; set; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public IDictionary<string, string> ToErrorDictionary() => new Dictionary<string, string>() { { Code, Message } };
}
