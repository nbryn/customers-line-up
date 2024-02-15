using FluentValidation;

namespace CLup.Domain.Shared.ValueObjects.Validation;

public class TimeIntervalValidator : AbstractValidator<TimeInterval>
{
    public TimeIntervalValidator()
    {
        RuleFor(interval => interval.Start).NotEmpty();
        RuleFor(interval => interval.Start.Hours).InclusiveBetween(0, 23);
        RuleFor(interval => interval.Start.Minutes).InclusiveBetween(0, 59);

        RuleFor(interval => interval.End).NotEmpty();
        RuleFor(interval => interval.End.Hours).InclusiveBetween(0, 23);
        RuleFor(interval => interval.End.Minutes).InclusiveBetween(0, 59);

        RuleFor(interval => new { interval.Start, interval.End }).Must(interval => interval.End > interval.Start);
    }
}
