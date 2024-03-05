using System;
using System.Collections.Generic;
using System.Linq;
using CLup.Application.Bookings;
using CLup.Application.Messages;
using CLup.Domain.Users;

namespace CLup.Application.Users;

public sealed class UserDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public string Email { get; init; }

    public string City { get; init; }

    public int Zip { get; init; }

    public string Street { get; init; }

    public double Longitude { get; init; }

    public double Latitude { get; init; }

    public string Role { get; init; }

    public IList<BookingDto> Bookings { get; init; }

    public IList<Guid> BusinessIds { get; init; }

    public IList<MessageDto> ReceivedMessages { get; init; }

    public IList<MessageDto> SentMessages { get; init; }

    public static UserDto FromUser(User user)
    {
        return new UserDto
        {
            Id = user.Id.Value,
            Name = user.UserData.Name,
            Email = user.UserData.Email,
            City = user.Address.City,
            Zip = user.Address.Zip,
            Street = user.Address.Street,
            Longitude = user.Coords.Longitude,
            Latitude = user.Coords.Latitude,
            Role = user.Role.ToString(),
            Bookings = user.Bookings.Select(BookingDto.FromBooking).ToList(),
            BusinessIds = user.Businesses.Select(business => business.Id.Value).ToList(),
            ReceivedMessages = user.ReceivedMessages
                    .Where(message => !message.Metadata.DeletedByReceiver)
                    .Select(MessageDto.FromMessage)
                    .ToList(),

            SentMessages = user.SentMessages
                .Where(message => !message.Metadata.DeletedBySender)
                .Select(MessageDto.FromMessage)
                .ToList(),
        };
    }
}
