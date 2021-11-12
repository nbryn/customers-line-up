using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Commands.Business.Create
{
    public class CreateBusinessCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
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

