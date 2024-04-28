namespace CLup.API.Bookings.Contracts.CreateBooking;

public sealed class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
{
    public CreateBookingRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotEmpty();
        RuleFor(request => request.TimeSlotId).NotEmpty();
    }
}
