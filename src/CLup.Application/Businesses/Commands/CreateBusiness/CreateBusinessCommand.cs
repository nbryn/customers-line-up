using System;
using CLup.Application.Shared;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Businesses.Commands.CreateBusiness;

public sealed class CreateBusinessCommand : IRequest<Result>
{
    public UserId OwnerId { get; set; }

    public string Name { get; set; }

    public string Zip { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public double Opens { get; set; }

    public double Closes { get; set; }

    public int Capacity { get; set; }

    public int TimeSlotLength { get; set; }

    public string Type { get; set; }
}
