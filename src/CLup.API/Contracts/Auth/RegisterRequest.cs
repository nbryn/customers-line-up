using CLup.Application.Auth.Commands.Register;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.API.Contracts.Auth;

public readonly record struct RegisterRequest(
    string Email,
    string Password,
    string Name,
    int Zip,
    string Street,
    string City,
    double? Longitude,
    double? Latitude)
{
    public RegisterCommand MapToCommand() =>
        new(new UserData(Name, Email, Password),
            new Address(Street, Zip, City),
            new Coords(Longitude!.Value, Latitude!.Value));
}
