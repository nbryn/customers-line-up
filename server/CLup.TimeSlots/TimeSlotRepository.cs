using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CLup.Context;
using CLup.Extensions;
using CLup.TimeSlots.DTO;
using CLup.TimeSlots.Interfaces;
using CLup.Util;

namespace CLup.TimeSlots
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;
        public TimeSlotRepository(
            CLupContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateTimeSlot(TimeSlot timeSlot)
        {
            await _context.TimeSlots.AddAsync(timeSlot);

            await _context.SaveChangesAsync();

            return new ServiceResponse<Guid>(HttpCode.Created, timeSlot.Id);
        }

        public async Task<ServiceResponse> DeleteTimeSlot(string timeSlotId)
        {
            TimeSlot timeSlot = await FindTimeSlotById(timeSlotId);

            if (timeSlot == null)
            {
                return new ServiceResponse(HttpCode.NotFound);
            }

            _context.TimeSlots.Remove(timeSlot);

            await _context.SaveChangesAsync();

            return new ServiceResponse(HttpCode.Deleted);
        }

        public async Task<TimeSlot> FindTimeSlotById(string timeSlotId)
        {
            TimeSlot timeSlot = await _context.TimeSlots.Include(x => x.Bookings)
                                                        .Include(x => x.Business)
                                                        .FirstOrDefaultAsync(x => x.Id.ToString() == timeSlotId);

            return timeSlot;
        }

        public async Task<ServiceResponse<IList<TimeSlotDTO>>> FindTimeSlotsByBusiness(string businessId)
        {
            IList<TimeSlot> timeSlots = await _context.TimeSlots.Include(x => x.Bookings)
                                                                .Include(x => x.Business)
                                                                .Where(x => x.BusinessId == businessId)
                                                                .ToListAsync();

            return this.AssembleResponse<TimeSlot, TimeSlotDTO>(timeSlots, _mapper);
        }

        public async Task<ServiceResponse<IList<TimeSlotDTO>>> FindTimeSlotsByBusinessAndDate(string businessId, DateTime date)
        {
            IList<TimeSlot> timeSlots = await _context.TimeSlots.Include(x => x.Bookings)
                                                                .Include(x => x.Business)
                                                                .Where(x => x.BusinessId == businessId && x.Start.Date == date.Date)
                                                                .ToListAsync();

            return this.AssembleResponse<TimeSlot, TimeSlotDTO>(timeSlots, _mapper);
        }

        public async Task<ServiceResponse<IList<TimeSlotDTO>>> FindAvailableTimeSlotsByBusiness(AvailableTimeSlotsRequest request)
        {
            IList<TimeSlot> timeSlots = await _context.TimeSlots.Include(x => x.Bookings)
                                                                .Include(x => x.Business)
                                                                .Where(x => x.BusinessId == request.BusinessId &&
                                                                               (x.Start > request.Start && x.End < request.End))
                                                                .ToListAsync();

            return this.AssembleResponse<TimeSlot, TimeSlotDTO>(timeSlots, _mapper);
        }

        public async Task<ServiceResponse> UpdateTimeSlot(TimeSlot updatedTimeSlot)
        {
            TimeSlot existingTimeSlot = await FindTimeSlotById(updatedTimeSlot.Id.ToString());

            if (existingTimeSlot == null)
            {
                return new ServiceResponse(HttpCode.NotFound);
            }

            updatedTimeSlot.Id = existingTimeSlot.Id;

            _context.Entry(existingTimeSlot).CurrentValues.SetValues(updatedTimeSlot);

            await _context.SaveChangesAsync();

            return new ServiceResponse(HttpCode.Updated);
        }
    }
}