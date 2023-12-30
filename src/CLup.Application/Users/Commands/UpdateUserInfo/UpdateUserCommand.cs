using CLup.Application.Shared;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Users.Commands.UpdateUserInfo;

public sealed class UpdateUserCommand : IRequest<Result>
{
    public UserId UserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Zip { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}
