using MediatR;

using CLup.Features.Common;

namespace CLup.Features.Businesses.Commands
{
    public class UpdateBusinessCommand : IRequest<Result>
    {
      
            public string Id { get; set; }
            public string Name { get; set; }
            public string Zip { get; set; }
            public string Address { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public string OwnerEmail { get; set; }
            public string Opens { get; set; }
            public string Closes { get; set; }
            public int Capacity { get; set; }
            public int TimeSlotLength { get; set; }
            public string Type { get; set; }
    }
}

