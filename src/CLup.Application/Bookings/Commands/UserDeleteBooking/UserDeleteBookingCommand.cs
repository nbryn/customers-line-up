using System;
using MediatR;
using CLup.Application.Shared.Result;

namespace CLup.Application.Bookings.Commands.UserDeleteBooking;

public class UserDeleteBookingCommand : IRequest<Result>
{
    public Guid UserId { get; set; }

    public Guid BookingId { get; set; }
}
