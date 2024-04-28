using CLup.Domain.TimeSlots;

namespace CLup.API.TimeSlots.Contracts.GenerateTimeSlots;

public class GenerateTimeSlotsRequestValidator : AbstractValidator<GenerateTimeSlotsRequest>
{
    public GenerateTimeSlotsRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotEmpty();
        RuleFor(request => request.Date)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage(TimeSlotErrors.TimeSlotCannotBeGeneratedOnDateInThePast.Message);
    }
}
