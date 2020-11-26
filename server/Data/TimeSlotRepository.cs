using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Logic.TimeSlots;
using Logic.Context;
using Logic.DTO;

namespace Data
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly ICLupContext _context;
        public TimeSlotRepository(ICLupContext context)
        {
            _context = context;
        }
        public async Task<int> CreateTimeSlot(TimeSlot timeSlot)
        {
            _context.TimeSlots.Add(timeSlot);

            await _context.SaveChangesAsync();

            return timeSlot.Id;
        }

        public async Task<TimeSlot> FindTimeSlotById(int timeSlotId)
        {
            TimeSlot timeSlot = await _context.TimeSlots.Include(x => x.Bookings)
                                                               .FirstOrDefaultAsync(x => x.Id == timeSlotId);

            return timeSlot;
        }

        public async Task<IList<TimeSlot>> FindTimeSlotsByBusiness(int businessId)
        {
            IList<TimeSlot> timeSlots = await _context.TimeSlots.Include(x => x.Bookings)
                                                                       .Where(x => x.BusinessId == businessId)
                                                                       .ToListAsync();
            return timeSlots;
        }

        public async Task<IList<TimeSlot>> FindAvailableTimeSlotsByBusiness(AvailableTimeSlotsRequest request)
        {
            IList<TimeSlot> timeSlots = await _context.TimeSlots.Include(x => x.Bookings)
                                                                        .Where(x => x.BusinessId == request.BusinessId &&
                                                                               (x.Start > request.Start && x.End < request.End))
                                                                        .ToListAsync();

            return timeSlots;
        }

        public async Task<int> UpdateTimeSlot(TimeSlot timeSlot)
        {
            _context.TimeSlots.Update(timeSlot);

            return await _context.SaveChangesAsync();
        }
    }
}