using CLup.Application.Bookings.Commands.BusinessDeleteBooking;
using CLup.Domain.Users.ValueObjects;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;
using BoId = CLup.Domain.Bookings.ValueObjects.BookingId;

namespace CLup.API.Bookings.Contracts.DeleteBusinessBooking;

public readonly record struct DeleteBusinessBookingRequest(Guid BusinessId, Guid BookingId)
{
    public DeleteBusinessBookingCommand MapToCommand(UserId userId) =>
        new(userId, BId.Create(BusinessId), BoId.Create(BookingId));
}
