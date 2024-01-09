using System;
using System.Threading.Tasks;
using CLup.Domain.Shared;
using FluentValidation;

namespace CLup.Application.Shared;

public class Result : DomainResult
{
    public HttpCode Code { get; private set; }

    protected Result(HttpCode code, Error? error = null) : base(error)
    {
        Code = code;
    }

    public ProblemDetails ToProblemDetails()
    {
        if (Success)
        {
            throw new InvalidOperationException("Can't convert result to problem details.");
        }

        return new ProblemDetails(Code, Error.ToErrorDictionary());
    }

    public static Result<T> ToResult<T>(T maybe, Error error) =>
        maybe == null ? NotFound<T>(error) : Ok(maybe);

    public static Result<T> Ok<T>(T value) => new(value, HttpCode.Ok);

    public static Result<T> Fail<T>(HttpCode code, Error error) => new(default, code, error);

    public static Result<T> NotFound<T>(Error error) => new(default, HttpCode.NotFound, error);

    public static Result<T> BadRequest<T>(Error error) => new(default, HttpCode.BadRequest, error);
}

public sealed class Result<T> : Result
{
    public T Value { get; private set; }

    protected internal Result(T value, HttpCode code, Error error = null)
        : base(code, error)
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

    public async Task<Result<U>> BindAsync<U>(Func<T, Task<U>> f)
    {
        if (Failure)
        {
            return Fail<U>(Code, Error);
        }

        var maybe = await f(Value);

        return Ok(maybe);
    }

    public Result<U> Bind<U>(Func<T, U> f, Error error)
    {
        var maybe = f(Value);

        if (maybe == null)
        {
            return NotFound<U>(error);
        }

        return Success ? Ok(maybe) : Fail<U>(Code, Error);
    }

    public async Task<Result<U>> Bind<U>(Func<T, Task<U>> f, Error error)
    {
        var maybe = await f(Value);

        if (maybe == null)
        {
            return NotFound<U>(error);
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
        HttpCode httpCode,
        Error? error = null)
    {
        if (Failure)
        {
            return Fail<T>(Code, Error);
        }

        if (!predicate(Value))
        {
            if (Value is DomainResult result)
            {
                return Fail<T>(httpCode, result.Error);
            }

            return Fail<T>(httpCode, error);
        }

        return await task;
    }

    public async Task<Result<T>> EnsureAsync(
        Task<Result<T>> task,
        Func<T, Task<bool>> predicate,
        HttpCode httpCode,
        Error? error = null)
    {
        if (Failure)
        {
            return Fail<T>(Code, Error);
        }

        if (!await predicate(Value))
        {
            if (Value is DomainResult result)
            {
                return Fail<T>(httpCode, result.Error);
            }

            return Fail<T>(httpCode, error);
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
        return !validationResult.IsValid
            ? BadRequest<T>(new Error(validationResult.Errors[0].PropertyName, validationResult.Errors[0].ErrorMessage))
            : Ok(Value);
    }
}
