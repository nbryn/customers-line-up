using System;
using CLup.Application.Shared;
using MediatR;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Bookings.Commands.UserDeleteBooking;

public sealed class UserDeleteBookingCommand : IRequest<Result>
{
    public UserId UserId { get; set; }

    public Guid BookingId { get; init; }

    public UserDeleteBookingCommand(UserId userId, Guid bookingId)
    {
        UserId = userId;
        BookingId = bookingId;
    }
}
