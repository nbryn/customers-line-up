using System.Collections.Generic;
using System.Threading.Tasks;
using System;

using Logic.Context;
using Logic.DTO;
using Logic.TimeSlots;

namespace Data
{
    public interface ITimeSlotRepository
    {
        Task<(int, HttpCode)> CreateTimeSlot(TimeSlot timeSlot);

        Task<IList<TimeSlot>> FindTimeSlotsByBusiness(int businessId);

        Task<IList<TimeSlot>> FindTimeSlotByBusinessAndDate(int businessId, DateTime date);

        Task<IList<TimeSlot>> FindAvailableTimeSlotsByBusiness(AvailableTimeSlotsRequest request);

        Task<TimeSlot> FindTimeSlotById(int timeSlotId);

        Task<HttpCode> UpdateTimeSlot(TimeSlot timeSlot);

        Task<HttpCode> DeleteTimeSlot(int timeSlotId);
        
    }
}