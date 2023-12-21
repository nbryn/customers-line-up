using System;
using MediatR;
using CLup.Application.Shared.Result;

namespace CLup.Application.Bookings.Commands.BusinessDeleteBooking;

public sealed class BusinessDeleteBookingCommand : IRequest<Result>
{
    public Guid OwnerId { get; set; }

    public Guid BookingId { get; set; }

    public Guid BusinessId { get; set; }

    public BusinessDeleteBookingCommand(
        Guid ownerId,
        Guid bookingId,
        Guid businessId)
    {
        OwnerId = ownerId;
        BookingId = bookingId;
        BusinessId = businessId;
    }
}
