using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.Context;
using Logic.DTO;
using Logic.TimeSlots;

namespace Data
{
    public interface ITimeSlotRepository
    {
        Task<(int, Response)> CreateTimeSlot(TimeSlot timeSlot);

        Task<IList<TimeSlot>> FindTimeSlotsByBusiness(int businessId);

        Task<IList<TimeSlot>> FindAvailableTimeSlotsByBusiness(AvailableTimeSlotsRequest request);

        Task<TimeSlot> FindTimeSlotById(int timeSlotId);

        Task<Response> UpdateTimeSlot(TimeSlot timeSlot);

        Task<Response> DeleteTimeSlot(int timeSlotId);
        
    }
}