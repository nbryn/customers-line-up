using CLup.Application.Bookings;
using CLup.Application.Messages;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users;
using CLup.Domain.Users.Enums;

namespace CLup.Application.Users;

public sealed class UserDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public string Email { get; init; }

    public Address Address { get; init; }

    public Role Role { get; init; }

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
            Address = user.Address,
            Role = user.Role,
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
