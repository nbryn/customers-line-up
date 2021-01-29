using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.Context;
using Logic.DTO;

namespace Logic.TimeSlots
{
    public interface ITimeSlotService
    {
        Task<Response> GenerateTimeSlots(CreateTimeSlotRequest request);

        Task<Response> RemoveTimeSlot(string userEmail, int timeSlotId);
    }
}