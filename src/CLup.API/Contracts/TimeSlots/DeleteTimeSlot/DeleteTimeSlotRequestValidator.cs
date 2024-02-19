using FluentValidation;

namespace CLup.API.Contracts.TimeSlots.DeleteTimeSlot;

public class DeleteTimeSlotRequestValidator : AbstractValidator<DeleteTimeSlotRequest>
{
    public DeleteTimeSlotRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotEmpty();
        RuleFor(request => request.TimeSlotId).NotEmpty();
    }
}
