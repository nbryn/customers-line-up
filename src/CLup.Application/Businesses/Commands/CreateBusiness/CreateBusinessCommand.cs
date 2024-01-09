using CLup.Application.Shared;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Businesses.Commands.CreateBusiness;

public sealed class CreateBusinessCommand : IRequest<Result>
{
    public UserId OwnerId { get; set; }

    public string Name { get; init; }

    public string Zip { get; init; }

    public string City { get; init; }

    public string Street { get; init; }

    public double Longitude { get; init; }

    public double Latitude { get; init; }

    public double Opens { get; init; }

    public double Closes { get; init; }

    public int Capacity { get; init; }

    public int TimeSlotLength { get; init; }

    public string Type { get; init; }
}
