using System.Collections.Generic;
using CLup.Application.Businesses.Queries;
using CLup.Application.Shared.Bookings;
using CLup.Application.Shared.Messages;

namespace CLup.Application.Users.Queries
{
    public class UserDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string Street { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Role { get; set; }

        public IList<BookingDto> Bookings { get; set; }

        public IList<BusinessDto> Businesses { get; set; }

        public IList<MessageDto> Messages { get; set; }
    }
}