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
                Address = business.Address,
                Opens = business.Opens,
                Closes = business.Closes,
                TimeSlotLength = business.TimeSlotLength,
                Capacity = business.Capacity,
                Type = business.Type.ToString("G")
            };
        }

        public BookingDTO ConvertBookingToDTO(Booking booking)
        {
            TimeSlot timeSlot = booking.TimeSlot;
            Business business = booking.TimeSlot.Business;
            return new BookingDTO
            {
                Id = booking.TimeSlotId,
                TimeSlotId = booking.TimeSlotId,
                Business = $"{timeSlot.BusinessName} - {business.Zip.Substring(0, business.Zip.IndexOf(" "))}",
                Address = business.Address,
                UserMail = booking.UserEmail,
                Date = timeSlot.Start.ToString("dd/MM/yyyy"),
                Interval = timeSlot.Start.TimeOfDay.ToString().Substring(0, 5) + " - " +
                           timeSlot.End.TimeOfDay.ToString().Substring(0, 5),
                Capacity = timeSlot.Bookings.Count().ToString() + "/" + timeSlot.Capacity
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