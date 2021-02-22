using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.Context;
using Logic.DTO;

namespace Logic.TimeSlots
{
    public interface ITimeSlotService
    {
        Task<QueryResult> GenerateTimeSlots(GenerateTimeSlotsRequest request);

        Task<HttpCode> RemoveTimeSlot(string userEmail, int timeSlotId);
    }
}