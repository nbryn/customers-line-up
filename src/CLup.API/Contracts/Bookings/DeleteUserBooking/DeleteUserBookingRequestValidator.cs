using FluentValidation;

namespace CLup.API.Contracts.Bookings.DeleteUserBooking;

public sealed class DeleteUserBookingRequestValidator : AbstractValidator<DeleteUserBookingRequest>
{
    public DeleteUserBookingRequestValidator()
    {
        RuleFor(request => request.BookingId).NotNull();
    }
}
