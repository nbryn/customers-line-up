using FluentValidation;

namespace CLup.API.Contracts.Bookings.CreateBooking;

public sealed class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
{
    public CreateBookingRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotEmpty();
        RuleFor(request => request.TimeSlotId).NotEmpty();
    }
}
