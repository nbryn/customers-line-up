using System.Threading.Tasks;
using System.Linq;

using Data;
using Logic.TimeSlots;
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
        public async Task<TimeSlotDTO> ConvertTimeSlotToDTO(TimeSlot timeSlot)
        {
            Business business = await _businessRepository.FindBusinessById(timeSlot.BusinessId);
            return new TimeSlotDTO
            {
                Id = timeSlot.Id,
                BusinessId = timeSlot.BusinessId,
                Business = business.Name,
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
                Opens = business.OpeningTime,
                Closes = business.ClosingTime,
                Type = business.Type
            };
        }
    }
}