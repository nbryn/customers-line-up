using System;
using System.Threading.Tasks;
using FluentValidation;

namespace CLup.Application.Shared
{
    public class Result
    {
        public bool Success { get; private set; }
        public string Error { get; private set; }
        public HttpCode Code { get; private set; }
        public bool Failure => !Success;

        protected Result(bool success, string error, HttpCode code)
        {
            Success = success;
            Error = error;
            Code = code;
        }

        public static Result<T> ToResult<T>(T maybe, string errorMessage) =>
            maybe == null ? NotFound<T>(errorMessage) : Ok(maybe);

        public static Result<T> Ok<T>(T value) => new(value, true, string.Empty, HttpCode.Ok);

        public static Result<T> Fail<T>(HttpCode code, string message) => new(default, false, message, code);

        public static Result<T> NotFound<T>(string message = "") => new(default, false, message, HttpCode.NotFound);

        public static Result<T> BadRequest<T>(string message = "") => new(default, false, message, HttpCode.BadRequest);
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }

        protected internal Result(T value, bool success, string error, HttpCode code)
            : base(success, error, code)
        {
            Value = value;
        }

        public Result<U> Bind<U>(Func<T, U> f) => Success ? Ok(f(Value)) : Fail<U>(Code, Error);

        public Result<T> AddDomainEvent(Action<T> f)
        {
            if (Failure)
            {
                return Fail<T>(Code, Error);
            }

            f(Value);

            return Ok(Value);
        }

        public async Task<Result<U>> Bind<U>(Func<T, Task<U>> f)
        {
            if (Failure)
            {
                return Fail<U>(Code, Error);
            }

            var maybe = await f(Value);

            return Ok(maybe);
        }

        public Result<U> Bind<U>(Func<T, U> f, string errorMessage)
        {
            var maybe = f(Value);

            if (maybe == null)
            {
                return NotFound<U>(errorMessage);
            }

            return Success ? Ok(maybe) : Fail<U>(Code, Error);
        }

        public async Task<Result<U>> Bind<U>(Func<T, Task<U>> f, string errorMessage)
        {
            var maybe = await f(Value);

            if (maybe == null)
            {
                return NotFound<U>(errorMessage);
            }

            return Success ? Ok(maybe) : Fail<U>(Code, Error);
        }

        public async Task<Result<T>> BindF<U>(Func<T, Task<U>> f)
        {
            if (Failure)
            {
                return Fail<T>(Code, Error);
            }

            await f(Value);

            return Ok(Value);
        }

        public async Task<Result<T>> Ensure(
            Task<Result<T>> task,
            Func<T, bool> predicate,
            string errorMessage,
            HttpCode httpCode)
        {
            if (Failure)
            {
                return Fail<T>(Code, Error);
            }

            if (!predicate(Value))
            {
                return Fail<T>(httpCode, errorMessage);
            }

            return await task;
        }

        public Result<T> Validate(IValidator<T> validator)
        {
            if (Failure)
            {
                return Fail<T>(Code, Error);
            }

            var validationResult = validator.Validate(Value);
            return !validationResult.IsValid ? BadRequest<T>(validationResult.Errors[0].ErrorMessage) : Ok(Value);
        }
    }
}