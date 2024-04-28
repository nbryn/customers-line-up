namespace CLup.API.Bookings.Contracts.DeleteUserBooking;

public sealed class DeleteUserBookingRequestValidator : AbstractValidator<DeleteUserBookingRequest>
{
    public DeleteUserBookingRequestValidator()
    {
        RuleFor(request => request.BookingId).NotEmpty();
    }
}
