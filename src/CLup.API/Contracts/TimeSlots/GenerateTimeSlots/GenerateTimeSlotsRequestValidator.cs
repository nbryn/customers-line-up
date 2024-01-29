using FluentValidation;

namespace CLup.API.Contracts.TimeSlots.GenerateTimeSlots;

public class GenerateTimeSlotsRequestValidator : AbstractValidator<GenerateTimeSlotsRequest>
{
    public GenerateTimeSlotsRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotNull();
        RuleFor(request => request.Start).NotNull();
    }
}
