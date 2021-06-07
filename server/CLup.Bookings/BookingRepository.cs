using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CLup.Bookings.DTO;
using CLup.Bookings.Interfaces;
using CLup.Context;
using CLup.Extensions;
using CLup.Util;

namespace CLup.Bookings
{
    public class BookingRepository : IBookingRepository
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public BookingRepository(
            CLupContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> SaveBooking(Booking booking)
        {
            _context.Bookings.Add(booking);

            await _context.SaveChangesAsync();

            return new ServiceResponse(HttpCode.Created);
        }

        public async Task<Booking> FindBookingByUserAndTimeSlot(string userEmail, string timeSlotId)
        {

            var booking = await _context.Bookings.Include(x => x.TimeSlot)
                                          .Include(x => x.TimeSlot.Business)
                                          .FirstOrDefaultAsync(x => x.TimeSlotId == timeSlotId
                                                                  && x.UserEmail == userEmail);



            return booking;
        }

        public async Task<ServiceResponse<IList<BookingDTO>>> FindBookingsByUser(string userEmail)
        {
            var bookings = await _context.Bookings.Include(x => x.TimeSlot)
                                          .Include(x => x.TimeSlot.Business)
                                          .Where(x => x.UserEmail.Equals(userEmail))
                                          .ToListAsync();


            return this.AssembleResponse<Booking, BookingDTO>(bookings, _mapper);
        }

        public async Task<ServiceResponse<IList<BookingDTO>>> FindBookingsByBusiness(string businessId)
        {
            var bookings = await _context.Bookings.Include(x => x.TimeSlot)
                                          .Include(x => x.TimeSlot.Business)
                                          .Where(x => x.BusinessId == businessId)
                                          .ToListAsync();

            return this.AssembleResponse<Booking, BookingDTO>(bookings, _mapper);
        }

        public async Task<ServiceResponse> DeleteBooking(string userEmail, string timeSlotId)
        {
            var booking = await FindBookingByUserAndTimeSlot(userEmail, timeSlotId);

            if (booking == null)
            {
                return new ServiceResponse(HttpCode.NotFound);
            }

            _context.Bookings.Remove(booking);

            await _context.SaveChangesAsync();

            return new ServiceResponse(HttpCode.Deleted);
        }


    }
}