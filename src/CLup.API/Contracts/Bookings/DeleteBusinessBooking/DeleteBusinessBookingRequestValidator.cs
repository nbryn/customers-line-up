namespace CLup.API.Contracts.Bookings.DeleteBusinessBooking;

public class DeleteBusinessBookingRequestValidator : AbstractValidator<DeleteBusinessBookingRequest>
{
    public DeleteBusinessBookingRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotEmpty();
        RuleFor(request => request.BookingId).NotEmpty();
    }
}
