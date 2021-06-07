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
        Task<ServiceResponse<Guid>> CreateTimeSlot(TimeSlot timeSlot);

        Task<ServiceResponse<IList<TimeSlotDTO>>> FindTimeSlotsByBusiness(string businessId);

        Task<ServiceResponse<IList<TimeSlotDTO>>> FindTimeSlotsByBusinessAndDate(string businessId, DateTime date);

        Task<ServiceResponse<IList<TimeSlotDTO>>> FindAvailableTimeSlotsByBusiness(AvailableTimeSlotsRequest request);

        Task<TimeSlot> FindTimeSlotById(string timeSlotId);

        Task<ServiceResponse> UpdateTimeSlot(TimeSlot timeSlot);

        Task<ServiceResponse> DeleteTimeSlot(string timeSlotId);
        
    }
}