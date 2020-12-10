using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

using Logic.TimeSlots;
using Logic.Businesses;
using Logic.Bookings;
using Logic.DTO.User;
using Logic.Users;
using Logic.DTO;

namespace Logic.Util
{
    public interface IDTOMapper
    {
       TimeSlotDTO ConvertTimeSlotToDTO(TimeSlot timeSlot);

       UserDTO ConvertUserToDTO(User user); 

       BusinessDTO ConvertBusinessToDTO(Business business);

       BookingDTO ConvertBookingToDTO(Booking booking);

       IEnumerable<string> GetBusinessTypes();
    }
}