using CLup.Domain.Shared;

namespace CLup.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new("Users.NotFound", $"The user was not found.");

    public static readonly Error InvalidCredentials = new("Users.InvalidCredentials", "Invalid credentials.");

    public static readonly Error BookingExists = new("Users.BookingExists", "You already have a booking for this time slot.");

    public static Error EmailExists(string email) =>
        new("Users.EmailExists", $"The email '{email}' is already in use.");
}
