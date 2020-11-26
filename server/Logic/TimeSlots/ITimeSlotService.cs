using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.DTO;

namespace Logic.TimeSlots
{
    public interface ITimeSlotService
    {
        Task<IEnumerable<TimeSlotDTO>> GenerateTimeSlots(CreateTimeSlotRequest request);

   

    }
}