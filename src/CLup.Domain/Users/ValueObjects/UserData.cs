using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Users.ValueObjects;

public sealed class UserData : ValueObject
{
    public string Name { get; }

    public string Email { get; }

    public string Password { get; }

    public UserData(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Email;
        yield return Password;
    }
}
