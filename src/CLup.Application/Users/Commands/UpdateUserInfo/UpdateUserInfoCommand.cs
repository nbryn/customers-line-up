using System;
using CLup.Application.Shared.Result;
using MediatR;

namespace CLup.Application.Users.Commands.UpdateUserInfo;

public sealed class UpdateUserInfoCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Zip { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}
