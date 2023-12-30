using System;
using CLup.Application.Shared;
using MediatR;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Businesses.Commands.UpdateBusiness;

public sealed class UpdateBusinessCommand : IRequest<Result>
{
    public Guid BusinessId { get; set; }

    public UserId OwnerId { get; set; }

    public string Name { get; set; }

    public string Zip { get; set; }

    public string Street { get; set; }

    public string City { get; set; }

    public string Opens { get; set; }

    public string Closes { get; set; }

    public int Capacity { get; set; }

    public int TimeSlotLength { get; set; }

    public string Type { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }
}
