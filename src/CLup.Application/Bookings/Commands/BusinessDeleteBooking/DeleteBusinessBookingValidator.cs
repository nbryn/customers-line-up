using FluentValidation;

namespace CLup.Application.Bookings.Commands.BusinessDeleteBooking;

public sealed class DeleteBusinessBookingValidator : AbstractValidator<DeleteBusinessBookingCommand>
{
    public DeleteBusinessBookingValidator()
    {
        RuleFor(command => command.BookingId).NotEmpty();
        RuleFor(command => command.BusinessId).NotEmpty();
    }
}
