using System.Collections.Generic;

using Logic.Businesses;
using Logic.Bookings;
using Logic.DTO;
using Logic.DTO.User;
using Logic.Employees;
using Logic.TimeSlots;
using Logic.Users;


namespace Logic.Util
{
    public interface IDTOMapper
    {
       TimeSlotDTO ConvertTimeSlotToDTO(TimeSlot timeSlot);

       UserDTO ConvertUserToDTO(User user); 

       BusinessDTO ConvertBusinessToDTO(Business business);

       BookingDTO ConvertBookingToDTO(Booking booking);

       EmployeeDTO ConvertEmployeeToDTO(Employee employee);

       IEnumerable<string> GetBusinessTypes();
    }
}