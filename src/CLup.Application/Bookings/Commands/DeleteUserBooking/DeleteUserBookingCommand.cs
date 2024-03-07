using CLup.Application.Shared;
using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Bookings.Commands.DeleteUserBooking;

public sealed class DeleteUserBookingCommand : IRequest<Result>
{
    public UserId UserId { get; }

    public BookingId BookingId { get; }

    public DeleteUserBookingCommand(UserId userId, BookingId bookingId)
    {
        UserId = userId;
        BookingId = bookingId;
    }
}
