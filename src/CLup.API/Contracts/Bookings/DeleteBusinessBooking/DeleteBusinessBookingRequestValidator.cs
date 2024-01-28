using FluentValidation;

namespace CLup.API.Contracts.Bookings.DeleteBusinessBooking;

public class DeleteBusinessBookingRequestValidator : AbstractValidator<DeleteBusinessBookingRequest>
{
    public DeleteBusinessBookingRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotNull();
        RuleFor(request => request.BookingId).NotNull();
    }
}
