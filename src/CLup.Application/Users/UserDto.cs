using System;
using System.Collections.Generic;
using CLup.Application.Bookings;
using CLup.Application.Messages;

namespace CLup.Application.Users;

public sealed class UserDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string City { get; set; }

    public string Zip { get; set; }

    public string Street { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public string Role { get; set; }

    public IList<BookingDto> Bookings { get; set; }

    public IList<Guid> BusinessIds { get; set; }

    public IList<MessageDto> ReceivedMessages { get; set; }

    public IList<MessageDto> SentMessages { get; set; }
}
