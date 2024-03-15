using CLup.Application.Bookings.Commands.BusinessDeleteBooking;
using CLup.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;
using BoId = CLup.Domain.Bookings.ValueObjects.BookingId;

namespace CLup.API.Contracts.Bookings.DeleteBusinessBooking;

public readonly record struct DeleteBusinessBookingRequest(Guid BusinessId, Guid BookingId)
{
    public DeleteBusinessBookingCommand MapToCommand(UserId userId) =>
        new(userId, BId.Create(BusinessId), BoId.Create(BookingId));
}
