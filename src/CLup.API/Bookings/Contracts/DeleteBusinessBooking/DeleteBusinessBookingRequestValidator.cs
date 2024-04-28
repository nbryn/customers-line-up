namespace CLup.API.Bookings.Contracts.DeleteBusinessBooking;

public class DeleteBusinessBookingRequestValidator : AbstractValidator<DeleteBusinessBookingRequest>
{
    public DeleteBusinessBookingRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotEmpty();
        RuleFor(request => request.BookingId).NotEmpty();
    }
}
