using FluentValidation;

namespace CLup.Domain.Shared.ValueObjects.Validation;

public class IntervalValidator : AbstractValidator<Interval>
{
    public IntervalValidator()
    {
        RuleFor(interval => interval.Start).NotEmpty();
        RuleFor(interval => interval.End).NotEmpty();

        RuleFor(interval => new { interval.Start, interval.End }).Must(interval => interval.End > interval.Start);
    }
}
