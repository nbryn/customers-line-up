using CLup.Domain.Shared;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(UserId userId) =>
        new("Users.NotFound", $"The user with the id {userId.Value} was not found.");

    public static Error EmailExists(string email) =>
        new("Users.EmailExists", $"The email '{email}' is already in use.");

    public static Error InvalidCredentials() =>
        new("Users.InvalidCredentials", "Invalid credentials.");

    public static Error BookingExists() =>
        new("Users.BookingExists", "You already have a booking for this time slot.");
}
