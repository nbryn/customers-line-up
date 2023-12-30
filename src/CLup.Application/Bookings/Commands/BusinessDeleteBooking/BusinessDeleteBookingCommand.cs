using System;
using CLup.Application.Shared;
using MediatR;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Bookings.Commands.BusinessDeleteBooking;

public sealed class BusinessDeleteBookingCommand : IRequest<Result>
{
    public UserId OwnerId { get; set; }

    public Guid BookingId { get; set; }

    public Guid BusinessId { get; set; }

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
