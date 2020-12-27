using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

using Data;

using Logic.Bookings;
using Logic.Businesses;
using Logic.DTO;
using Logic.DTO.User;
using Logic.Employees;
using Logic.TimeSlots;
using Logic.Users;



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
                Capacity = timeSlot.Bookings.Count() + "/" + timeSlot.Capacity.ToString(),
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
                Capacity = business.Capacity,
                Type = business.Type.ToString("G")
            };
        }

        public BookingDTO ConvertBookingToDTO(Booking booking)
        {
            return new BookingDTO
            {
                Id = booking.TimeSlotId,
                TimeSlotId = booking.TimeSlotId,
                Business = booking.TimeSlot.BusinessName,
                UserMail = booking.UserEmail,
                Date = booking.TimeSlot.Start.ToString("dd/MM/yyyy"),
                Interval = booking.TimeSlot.Start.TimeOfDay.ToString().Substring(0, 5) + " - " +
                           booking.TimeSlot.End.TimeOfDay.ToString().Substring(0, 5),
                Capacity = booking.TimeSlot.Bookings.Count().ToString() + "/" + booking.TimeSlot.Capacity
            };
        }

        public EmployeeDTO ConvertEmployeeToDTO(Employee employee)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.User.Name,
                PrivateEmail = employee.UserEmail,
                CompanyEmail = employee.CompanyEmail,
                Business = employee.Business.Name,
                BusinessId = employee.BusinessId,
                EmployedSince = employee.CreatedAt.ToString("dd/MM/yyyy")
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