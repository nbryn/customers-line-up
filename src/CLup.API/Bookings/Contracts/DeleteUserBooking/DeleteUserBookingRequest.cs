using CLup.Application.Bookings.Commands.DeleteUserBooking;
using CLup.Domain.Users.ValueObjects;
using BId = CLup.Domain.Bookings.ValueObjects.BookingId;

namespace CLup.API.Bookings.Contracts.DeleteUserBooking;

public readonly record struct DeleteUserBookingRequest(Guid BookingId)
{
    public DeleteUserBookingCommand MapToCommand(UserId userId) => new(userId, BId.Create(BookingId));
}
