using System.Threading.Tasks;
using System.Linq;

using Logic.TimeSlots;
using Logic.Businesses;
using Logic.DTO.User;
using Logic.Users;
using Logic.DTO;

namespace Logic.Util
{
    public interface IDTOMapper
    {
       Task<TimeSlotDTO> ConvertTimeSlotToDTO(TimeSlot timeSlot);

       UserDTO ConvertUserToDTO(User user); 

       BusinessDTO ConvertBusinessToDTO(Business business);
    }
}