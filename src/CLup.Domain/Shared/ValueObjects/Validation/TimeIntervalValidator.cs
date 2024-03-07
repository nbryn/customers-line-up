namespace CLup.Domain.Shared.ValueObjects.Validation;

public class TimeIntervalValidator : AbstractValidator<TimeInterval>
{
    public TimeIntervalValidator()
    {
        RuleFor(interval => interval.Start).NotEmpty();
        RuleFor(interval => interval.Start.Second).Must(second => second == 0);
        RuleFor(interval => interval.Start.Minute).InclusiveBetween(0, 59);
        RuleFor(interval => interval.Start.Hour).InclusiveBetween(0, 23);

        RuleFor(interval => interval.End).NotEmpty();
        RuleFor(interval => interval.End.Second).Must(second => second == 0);
        RuleFor(interval => interval.End.Minute).InclusiveBetween(0, 59);
        RuleFor(interval => interval.End.Hour).InclusiveBetween(0, 23);

        RuleFor(interval => interval.End.Hour).InclusiveBetween(0, 59);
        RuleFor(interval => new { interval.Start, interval.End }).Must(interval => interval.End > interval.Start);
    }
}
