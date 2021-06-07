using System.Threading.Tasks;

using CLup.TimeSlots.DTO;
using CLup.Util;

namespace CLup.TimeSlots.Interfaces
{
    public interface ITimeSlotService
    {
        Task<ServiceResponse> GenerateTimeSlots(GenerateTimeSlotsRequest request);

        Task<ServiceResponse> RemoveTimeSlot(string userEmail, string timeSlotId);
    }
}