using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users;
using CLup.Domain.Users.Enums;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Infrastructure.Persistence.Seed.Builders;

public sealed class UserBuilder
{
    private UserData _userData;
    private Address _address;
    private Coords _coords;
    private Role _role = Role.User;

    public UserBuilder()
    {

    }

    public UserBuilder WithUserData(string name, string email, string password)
    {
        _userData = new UserData(name, email, password);

        return this;
    }

    public UserBuilder WithAddress(string street, string zip, string city)
    {
        _address = new Address(street, zip, city);

        return this;
    }

    public UserBuilder WithCoords(double longitude, double latitude)
    {
        _coords = new Coords(longitude, latitude);

        return this;
    }

    public UserBuilder WithRole(Role role)
    {
        _role = role;

        return this;
    }

    public User Build() => new User(_userData, _address, _coords, _role);
}
