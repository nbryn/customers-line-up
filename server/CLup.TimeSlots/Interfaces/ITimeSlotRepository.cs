using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CLup.Context;
using CLup.TimeSlots.DTO;
using CLup.Util;

namespace CLup.TimeSlots.Interfaces
{
    public interface ITimeSlotRepository : IRepository<TimeSlot>
    {
        Task<ServiceResponse<int>> CreateTimeSlot(TimeSlot timeSlot);

        Task<ServiceResponse<IList<TimeSlotDTO>>> FindTimeSlotsByBusiness(int businessId);

        Task<ServiceResponse<IList<TimeSlotDTO>>> FindTimeSlotsByBusinessAndDate(int businessId, DateTime date);

        Task<ServiceResponse<IList<TimeSlotDTO>>> FindAvailableTimeSlotsByBusiness(AvailableTimeSlotsRequest request);

        Task<TimeSlot> FindTimeSlotById(int timeSlotId);

        Task<ServiceResponse> UpdateTimeSlot(TimeSlot timeSlot);

        Task<ServiceResponse> DeleteTimeSlot(int timeSlotId);
        
    }
}