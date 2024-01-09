using System;
using CLup.Application.Shared;
using MediatR;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Businesses.Commands.UpdateBusiness;

public sealed class UpdateBusinessCommand : IRequest<Result>
{
    public UserId OwnerId { get; set; }

    public Guid BusinessId { get; init; }

    public string Name { get; init; }

    public string Zip { get; init; }

    public string Street { get; init; }

    public string City { get; init; }

    public string Opens { get; init; }

    public string Closes { get; init; }

    public int Capacity { get; init; }

    public int TimeSlotLength { get; init; }

    public string Type { get; init; }

    public double Longitude { get; init; }

    public double Latitude { get; init; }
}
