using System;
using System.Threading.Tasks;
using FluentValidation;

namespace CLup.Application.Shared.Extensions
{
    public static class TaskExtensions
    {

        public static async Task<Result<T>> FailureIf<T>(this Task<T> task, string errorMessage = "")
        {
            var maybe = await task;

            return Result.ToResult(maybe, errorMessage);
        }

        public static async Task<Result<T>> ToResult<T>(this Task<T> task)
        {
            var maybe = await task;

            return Result.Ok(maybe);
        }

        public static async Task<Result<T>> AddDomainEvent<T>(this Task<Result<T>> task, Action<T> f)
            => (await task).AddDomainEvent(f);

        public static async Task<Result<T>> Validate<T>(this Task<Result<T>> task, IValidator<T> validator)
            => (await task).Validate(validator);

        public static async Task<Result<T>> FailureIf<T>(this Task<Result> task, Func<Task<T>> f, string errorMessage = "")
            => await (await task).Bind(f, errorMessage);

        public static async Task<Result<U>> FailureIf<T, U>(this Task<Result<T>> task, Func<T, U> f, string errorMessage = "")
            => (await task).Bind(f, errorMessage);
        
        public static async Task<Result<T>> AndThen<T>(this Task<Result> task, Func<T> f)
            => (await task).Bind(f);
        
        public static async Task<Result<T>> AndThen<T>(this Task<Result> task, Func<Task<T>> f)
            => await (await task).Bind(f);
            
        public static async Task<Result<U>> AndThen<T, U>(this Task<Result<T>> task, Func<T, U> f)
            => (await task).Bind(f);

        public static async Task<Result> AndThenDiscard<T>(this Task<Result<T>> task, Action<T> f)
            => (await task).BindIgnore(f);

        public static async Task<Result<U>> AndThen<T, U>(this Task<Result<T>> task, Func<T, Task<U>> f)
            => await (await task).Bind(f);

        public static async Task<Result<U>> Finally<T, U>(this Task<Result<T>> task, Func<T, U> f)
            => (await task).Bind(f);

        public static async Task<Result<U>> Finally<T, U>(this Task<Result<T>> task, Func<T, Task<U>> f)
            => await (await task).Bind(f);

        public static async Task<Result<T>> AndThenF<T, U>(this Task<Result<T>> task, Func<T, Task<U>> f)
            => await (await task).BindF(f);

        public static async Task<Result<T>> Ensure<T>(this Task<Result<T>> task, Func<T, bool> predicate, (HttpCode code, string message) errorInfo)
            => await (await task).Ensure(task, predicate, errorInfo);

        public static async Task<Result> EnsureDiscard<T>(this Task<Result<T>> task, Func<T, bool> predicate, string errorMessage = "")
            => (await task).EnsureIgnore(predicate, errorMessage);
    }
}