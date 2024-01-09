using System;
using CLup.Application.Shared;
using MediatR;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Bookings.Commands.BusinessDeleteBooking;

public sealed class BusinessDeleteBookingCommand : IRequest<Result>
{
    public UserId OwnerId { get; set; }

    public Guid BookingId { get; init; }

    public Guid BusinessId { get; init; }

    public BusinessDeleteBookingCommand(
        UserId ownerId,
        Guid bookingId,
        Guid businessId)
    {
        OwnerId = ownerId;
        BookingId = bookingId;
        BusinessId = businessId;
    }
}
