using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

using Data;
using Logic.TimeSlots;
using Logic.Bookings;
using Logic.Businesses;
using Logic.DTO.User;
using Logic.Users;
using Logic.DTO;


namespace Logic.Util
{
    public class DTOMapper : IDTOMapper
    {
        private readonly IBusinessRepository _businessRepository;

        public DTOMapper(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }
        public TimeSlotDTO ConvertTimeSlotToDTO(TimeSlot timeSlot)
        {
            return new TimeSlotDTO
            {
                Id = timeSlot.Id,
                BusinessId = timeSlot.BusinessId,
                Business = timeSlot.BusinessName,
                Date = timeSlot.Start.ToString("dd/MM/yyyy"),
                Start = timeSlot.Start.TimeOfDay.ToString().Substring(0, 5),
                End = timeSlot.End.TimeOfDay.ToString().Substring(0, 5),
            };
        }

        public UserDTO ConvertUserToDTO(User user)
        {
            return new UserDTO
            {
                Name = user.Name,
                Email = user.Email,
                Zip = user.Zip,
            };
        }

        public BusinessDTO ConvertBusinessToDTO(Business business)
        {
            return new BusinessDTO
            {
                Id = business.Id,
                Name = business.Name,
                Zip = business.Zip,
                Opens = business.Opens,
                Closes = business.Closes,
                TimeSlotLength = business.TimeSlotLength,
                Type = business.Type.ToString("G")
            };
        }

        public BookingDTO ConvertBookingToDTO(Booking booking)
        {
            return new BookingDTO
            {
                TimeSlotId = booking.TimeSlotId,
                BusinessId = booking.BusinessId,
                UserMail = booking.UserEmail,
                StartTime = booking.TimeSlot.Start.TimeOfDay.ToString().Substring(0, 5),
                EndTime = booking.TimeSlot.End.TimeOfDay.ToString().Substring(0, 5),
                NumberOfUsersWithSameBooking = booking.TimeSlot.Bookings.Count(),
            };
        }

        public IEnumerable<string> GetBusinessTypes()
        {
            List<string> values = new List<string>();
            var types = EnumUtil.GetValues<BusinessType>();

            foreach (BusinessType type in types)
            {
                values.Add(type.ToString("G"));
            }

            return values;
        }
    }
}