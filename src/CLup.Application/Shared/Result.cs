using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLup.Domain.Shared;
using FluentValidation;

namespace CLup.Application.Shared;

public class Result : DomainResult
{
    public HttpCode Code { get; private set; }

    protected Result(HttpCode code, List<Error> errors) : base(errors)
    {
        Code = code;
    }

    public ProblemDetails ToProblemDetails()
    {
        if (Success)
        {
            throw new InvalidOperationException("Can't convert result to problem details.");
        }

        return new ProblemDetails(Code,
            Errors.ToDictionary(k => k.Code, error => new List<string>() { error.Message }));
    }

    public static Result<T> ToResult<T>(T maybe, List<Error> errors) =>
        maybe == null ? NotFound<T>(errors) : Ok(maybe);

    public static Result<T> Ok<T>(T value) => new(value, HttpCode.Ok, new List<Error>());

    public static Result<T> Fail<T>(HttpCode code, List<Error> errors) => new(default, code, errors);

    public static Result<T> NotFound<T>(List<Error> errors) => new(default, HttpCode.NotFound, errors);

    public static Result<T> BadRequest<T>(List<Error> errors) => new(default, HttpCode.BadRequest, errors);
}

public sealed class Result<T> : Result
{
    public T Value { get; private set; }

    protected internal Result(T value, HttpCode code, List<Error> errors)
        : base(code, errors)
    {
        Value = value;
    }

    public Result<U> Bind<U>(Func<T, U> f) => Success ? Ok(f(Value)) : Fail<U>(Code, Errors);

    public Result<T> AddDomainEvent(Action<T> f)
    {
        if (Failure)
        {
            return Fail<T>(Code, Errors);
        }

        f(Value);

        return Ok(Value);
    }

    public async Task<Result<U>> BindAsync<U>(Func<T, Task<U>> f)
    {
        if (Failure)
        {
            return Fail<U>(Code, Errors);
        }

        var maybe = await f(Value);

        return Ok(maybe);
    }

    public Result<U> Bind<U>(Func<T, U> f, Error error)
    {
        var maybe = f(Value);
        if (maybe == null)
        {
            Errors.Add(error);
            return NotFound<U>(Errors);
        }

        if (Failure)
        {
            return Fail<U>(Code, Errors);
        }

        return Success ? Ok(maybe) : Fail<U>(Code, Errors);
    }

    public async Task<Result<U>> Bind<U>(Func<T, Task<U>> f, Error error)
    {
        var maybe = await f(Value);
        if (maybe == null)
        {
            Errors.Add(error);
            return NotFound<U>(Errors);
        }

        if (Failure)
        {
            return Fail<U>(Code, Errors);
        }

        return Success ? Ok(maybe) : Fail<U>(Code, Errors);
    }

    public async Task<Result<T>> BindF<U>(Func<T, Task<U>> f)
    {
        if (Failure)
        {
            return Fail<T>(Code, Errors);
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
        if (!predicate(Value))
        {
            Errors.Add(error);
            return Fail<T>(httpCode, Errors);
        }

        if (Failure)
        {
            return Fail<T>(Code, Errors);
        }

        return await task;
    }

    public async Task<Result<T>> EnsureAsync(
        Task<Result<T>> task,
        Func<T, Task<bool>> predicate,
        HttpCode httpCode,
        Error? error = null)
    {
        if (!await predicate(Value))
        {
            Errors.Add(error);
            return Fail<T>(httpCode, Errors);
        }

        if (Failure)
        {
            return Fail<T>(Code, Errors);
        }

        return await task;
    }

    public Result<T> Validate(IValidator<T> validator)
    {
        if (Failure)
        {
            return Fail<T>(Code, Errors);
        }

        var validationResult = validator.Validate(Value);
        return !validationResult.IsValid
            ? BadRequest<T>(validationResult.Errors.Select(error => new Error(error.PropertyName, error.ErrorMessage))
                .ToList())
            : Ok(Value);
    }
}
