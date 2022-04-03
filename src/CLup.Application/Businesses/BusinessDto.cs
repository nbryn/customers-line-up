using System.Collections.Generic;
using CLup.Application.Businesses.Employees.Queries;
using CLup.Application.Businesses.TimeSlots.Queries;
using CLup.Application.Shared.Bookings;
using CLup.Application.Shared.Messages;

namespace CLup.Application.Businesses
{
    public class BusinessDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Opens { get; set; }
        public string Closes { get; set; }
        public int TimeSlotLength { get; set; }
        public string BusinessHours { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
        public string OwnerEmail { get; set; }

        public IList<BookingDto> Bookings { get; set; }

        public IList<EmployeeDto> Employees { get; set; }

        public IList<MessageDto> Messages { get; set; }

        public IList<TimeSlotDto> TimeSlots { get; set; }  
    }
}