using CLup.Domain.Shared;

namespace CLup.Domain.Users;

public static class UserErrors
{
    public static Error NotFound => new("Users.NotFound", $"The user was not found.");

    public static Error InvalidCredentials => new("Users.InvalidCredentials", "Invalid credentials.");

    public static Error BookingExists => new("Users.BookingExists", "You already have a booking for this time slot.");

    public static Error EmailExists(string email) =>
        new("Users.EmailExists", $"The email '{email}' is already in use.");
}
