using FluentValidation;

namespace CLup.Application.Bookings.Commands.DeleteBooking;

public class BusinessDeleteBookingValidator : AbstractValidator<BusinessDeleteBookingCommand>
{
    public BusinessDeleteBookingValidator()
    {
        RuleFor(command => command.OwnerId).NotNull();
        RuleFor(command => command.BookingId).NotNull();
        RuleFor(command => command.BusinessId).NotNull();
    }
}
