using FluentValidation;

namespace CLup.Application.Bookings.Commands.DeleteUserBooking;

public sealed class DeleteUserBookingValidator : AbstractValidator<DeleteUserBookingCommand>
{
    public DeleteUserBookingValidator()
    {
        RuleFor(command => command.BookingId).NotNull();
    }
}
