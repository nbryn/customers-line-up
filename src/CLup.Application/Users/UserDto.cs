using System;
using System.Collections.Generic;
using System.Linq;
using CLup.Application.Bookings;
using CLup.Application.Messages;
using CLup.Domain.Users;

namespace CLup.Application.Users;

public sealed class UserDto
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public string City { get; private set; }

    public string Zip { get; private set; }

    public string Street { get; private set; }

    public double Longitude { get; private set; }

    public double Latitude { get; private set; }

    public string Role { get; private set; }

    public IList<BookingDto> Bookings { get; private set; }

    public IList<Guid> BusinessIds { get; private set; }

    public IList<MessageDto> ReceivedMessages { get; private set; }

    public IList<MessageDto> SentMessages { get; private set; }

    public UserDto FromUser(User user)
    {
        Id = user.Id.Value;
        Name = user.UserData.Name;
        Email = user.UserData.Email;
        City = user.Address.City;
        Zip = user.Address.Zip;
        Street = user.Address.Street;
        Longitude = user.Coords.Longitude;
        Latitude = user.Coords.Latitude;
        Role = user.Role.ToString();

        Bookings = user.Bookings.Select(new BookingDto().FromBooking).ToList();
        BusinessIds = user.Businesses.Select(business => business.Id.Value).ToList();
        ReceivedMessages = user.ReceivedMessages.Select(new MessageDto().FromMessage).ToList();
        SentMessages = user.SentMessages.Select(new MessageDto().FromMessage).ToList();

        return this;
    }
}
