using System;
using CLup.Application.Shared;
using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Businesses.ValueObjects;
using MediatR;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Bookings.Commands.BusinessDeleteBooking;

public sealed class DeleteBusinessBookingCommand : IRequest<Result>
{
    public UserId OwnerId { get; set; }

    public BookingId BookingId { get; init; }

    public BusinessId BusinessId { get; init; }

    public DeleteBusinessBookingCommand(
        UserId ownerId,
        BusinessId businessId,
        BookingId bookingId)
    {
        OwnerId = ownerId;
        BusinessId = businessId;
        BookingId = bookingId;
    }
}
