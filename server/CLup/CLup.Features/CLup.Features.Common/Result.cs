using System;

namespace CLup.Features.Common
{
    public class Result
    {
        public bool Success { get; private set; }
        public string Error { get; private set; }
        public HttpCode Code { get; private set; }
        public bool Failure
        {
            get { return !Success; }
        }
        protected Result(bool success, string error, HttpCode code)
        {
            Success = success;
            Error = error;
            Code = code;
        }

        public static Result Ok()
        {
            return new Result(true, String.Empty, HttpCode.Ok);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, String.Empty, HttpCode.Ok);
        }
        public static Result Fail(HttpCode code, string message)
        {
            return new Result(false, message, code);
        }

        public static Result<T> Fail<T>(HttpCode code, string message)
        {
            return new Result<T>(default(T), false, message, code);
        }

        public static Result NotFound(string message = "")
        {
            return new Result(false, message, HttpCode.NotFound);
        }

        public static Result<T> NotFound<T>()
        {
            return new Result<T>(default(T), false, String.Empty, HttpCode.NotFound);
        }

        public static Result Forbidden(string message)
        {
            return new Result(false, message, HttpCode.Forbidden);
        }

        public static Result Conflict(string message = "")
        {
            return new Result(true, message, HttpCode.Conflict);
        }

        public static Result<T> Conflict<T>(string message = "")
        {
            return new Result<T>(default(T), false, message, HttpCode.Conflict);
        }

        public static Result<T> Unauthorized<T>()
        {
            return new Result<T>(default(T), false, String.Empty, HttpCode.Unauthorized);
        }
    }
    public class Result<T> : Result
    {
        public T Value { get; set; }

        protected internal Result(T value, bool success, string error, HttpCode code)
            : base(success, error, code)
        {
            Value = value;
        }
    }
}