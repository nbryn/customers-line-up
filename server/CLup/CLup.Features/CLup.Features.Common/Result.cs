using System;
using System.Threading.Tasks;

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

        public async Task<Result<T>> Bind<T>(Task<Result> result, Func<Task<T>> f, string errorMessage)
        {
            var maybe = await f();

            if (maybe == null)
            {
                return NotFound<T>(errorMessage);
            }

            return Success ? Ok<T>(maybe) : Fail<T>(Code, Error);
        }

        public async Task<Result> BindIgnore<T>(Func<Task<T>> f, string errorMessage)
        {
            if (Success)
            {
                var maybe = await f();

                return maybe == null ? NotFound(errorMessage) : Ok();
            }

            return Fail(Code, Error);
        }

        public Result<T> Bind<T>(Task<Result> result, Func<T> f) => Success ? Ok<T>(f()) : Fail<T>(Code, Error);

        public static Result<T> ToResult<T>(T maybe, string errorMessage) => maybe == null ? NotFound<T>(errorMessage) : Ok<T>(maybe);

        public static Result ToResultIgnore<T>(T maybe, string errorMessage) => maybe == null ? NotFound(errorMessage) : Ok();

        public static Result Ok() => new Result(true, String.Empty, HttpCode.Ok);

        public static Result<T> Ok<T>(T value) => new Result<T>(value, true, String.Empty, HttpCode.Ok);

        public static Result Fail(HttpCode code, string message) => new Result(false, message, code);

        public static Result<T> Fail<T>(HttpCode code, string message) => new Result<T>(default(T), false, message, code);

        public static Result NotFound(string message = "") => new Result(false, message, HttpCode.NotFound);

        public static Result<T> NotFound<T>(string message = "") => new Result<T>(default(T), false, message, HttpCode.NotFound);

        public static Result Forbidden(string message) => new Result(false, message, HttpCode.Forbidden);

        public static Result Conflict(string message = "") => new Result(false, message, HttpCode.Conflict);

        public static Result<T> Conflict<T>(string message = "") => new Result<T>(default(T), false, message, HttpCode.Conflict);

        public static Result<T> Unauthorized<T>() => new Result<T>(default(T), false, String.Empty, HttpCode.Unauthorized);
    }
    public class Result<T> : Result
    {
        public T Value { get; set; }

        protected internal Result(T value, bool success, string error, HttpCode code)
            : base(success, error, code)
        {
            Value = value;
        }

        public Result<U> Bind<U>(Task<Result<T>> result, Func<T, U> f) => Success ? Ok<U>(f(Value)) : Fail<U>(Code, Error);

        public async Task<Result<U>> Bind<U>(Func<T, Task<U>> f)
        {
            if (Failure)
            {
                return Fail<U>(Code, Error);
            }

            var maybe = await f(Value);

            return Ok<U>(maybe);
        }

        public async Task<Result<T>> BindF<U>(Func<T, Task<U>> f)
        {
            if (Failure)
            {
                return Fail<T>(Code, Error);
            }

            await f(Value);

            return Ok<T>(Value);
        }

        public Result<T> BindF(Action<T> f)
        {
            if (Failure)
            {
                return Fail<T>(Code, Error);
            }

            f(Value);

            return Ok<T>(Value);
        }

        public async Task<Result<T>> BindF(Func<T, Task> f)
        {
            if (Failure)
            {
                return Fail<T>(Code, Error);
            }

            await f(Value);

            return Ok<T>(Value);
        }

        public async Task<Result> BindIgnore(Func<T, Task> f)
        {
            if (Success)
            {
                await f(Value);

                return Ok();
            }

            return Fail(Code, Error);
        }

        public Result BindIgnore(Action<T> f)
        {
            if (Success)
            {
                f(Value);

                return Ok();
            }

            return Fail(Code, Error);
        }

        public async Task<Result<T>> Ensure(Task<Result<T>> task, Func<T, bool> predicate, string errorMessage)
        {
            if (Failure)
            {
                return Fail<T>(Code, Error);
            }

            if (!predicate(Value))
            {
                return Conflict<T>(errorMessage);
            }

            return await task;
        }

        public async Task<Result<T>> Ensure(Task<Result<T>> task, Func<T, bool> predicate, (HttpCode code, string message) errorInfo)
        {
            if (Failure)
            {
                return Fail<T>(Code, Error);
            }

            if (!predicate(Value))
            {
                return Fail<T>(errorInfo.code, errorInfo.message);
            }

            return await task;
        }

        public Result EnsureIgnore(Task<Result<T>> task, Func<T, bool> predicate, string errorMessage)
        {
            if (Failure)
            {
                return Fail(Code, Error);
            }

            if (!predicate(Value))
            {
                return Conflict(errorMessage);
            }

            return Ok();
        }

        public Result EnsureIgnore(Task<Result<T>> task, Func<T, bool> predicate, (HttpCode code, string message) errorInfo)
        {
            if (Failure)
            {
                return Fail(Code, Error);
            }

            if (!predicate(Value))
            {
                return Fail(errorInfo.code, errorInfo.message);
            }

            return Ok();
        }
    }
}