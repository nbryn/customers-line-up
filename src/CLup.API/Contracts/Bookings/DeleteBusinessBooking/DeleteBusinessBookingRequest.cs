using CLup.Application.Bookings.Commands.BusinessDeleteBooking;
using CLup.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;
using BoId = CLup.Domain.Bookings.ValueObjects.BookingId;

namespace CLup.API.Contracts.Bookings.DeleteBusinessBooking;

public record struct DeleteBusinessBookingRequest
{
    [FromQuery] public Guid BusinessId { get; set; }

    [FromRoute] public Guid BookingId { get; set; }

    public DeleteBusinessBookingCommand MapToCommand(UserId userId) =>
        new(userId, BId.Create(BusinessId), BoId.Create(BookingId));
}
