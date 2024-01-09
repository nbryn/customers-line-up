using System;
using System.Threading.Tasks;
using FluentValidation;
using CLup.Domain.Shared;

namespace CLup.Application.Shared.Extensions;

public static class TaskExtensions
{
    public static async Task<Result<T>> ToResult<T>(this Task<T> task)
    {
        var maybe = await task;

        return Result.Ok(maybe);
    }

    public static async Task<Result<T>> FailureIfNotFound<T>(this Task<T> task, Error error)
    {
        var maybe = await task;

        return Result.ToResult(maybe, error);
    }

    public static async Task<Result<U>> FailureIfNotFound<T, U>(
        this Task<Result<T>> task,
        Func<T, U> f,
        Error error)
        => (await task).Bind(f, error);

    public static async Task<Result<U>> FailureIfNotFoundAsync<T, U>(
        this Task<Result<T>> task,
        Func<T, Task<U>> f,
        Error error)
        => await (await task).Bind(f, error);

    public static async Task<Result<T>> AddDomainEvent<T>(this Task<Result<T>> task, Action<T> f) =>
        (await task).AddDomainEvent(f);

    public static async Task<Result<T>> Validate<T>(this Task<Result<T>> task, IValidator<T> validator)
        => (await task).Validate(validator);

    public static async Task<Result<U>> AndThen<T, U>(this Task<Result<T>> task, Func<T, U> f) => (await task).Bind(f);

    public static async Task<Result<U>> AndThenAsync<T, U>(this Task<Result<T>> task, Func<T, Task<U>> f) =>
        await (await task).BindAsync(f);

    public static async Task<Result<T>> AndThenF<T, U>(this Task<Result<T>> task, Func<T, Task<U>> f)
        => await (await task).BindF(f);

    public static async Task<Result<T>> Ensure<T>(
        this Task<Result<T>> task,
        Func<T, bool> predicate,
        HttpCode httpCode,
        Error? error = null)
        => await (await task).Ensure(task, predicate, httpCode, error);

    public static async Task<Result<T>> EnsureAsync<T>(
        this Task<Result<T>> task,
        Func<T, Task<bool>> predicate,
        HttpCode httpCode,
        Error? error = null)
        => await (await task).EnsureAsync(task, predicate, httpCode, error);

    public static async Task<Result<U>> Finally<T, U>(this Task<Result<T>> task, Func<T, U> f)
        => (await task).Bind(f);

    public static async Task<Result<U>> FinallyAsync<T, U>(this Task<Result<T>> task, Func<T, Task<U>> f)
        => await (await task).BindAsync(f);
}
