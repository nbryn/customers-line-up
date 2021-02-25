using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Logic.Bookings;
using Logic.Context;
using Logic.DTO;

namespace Data
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ICLupContext _context;

        public BookingRepository(ICLupContext context)
        {
            _context = context;
        }
        public async Task<HttpCode> SaveBooking(Booking booking)
        {
            _context.Bookings.Add(booking);

            await _context.SaveChangesAsync();

            return HttpCode.Created;
        }

        public async Task<Booking> FindBookingByUserAndTimeSlot(string userEmail, int timeSlotId)
        {
            return await _context.Bookings.Include(x => x.TimeSlot)
                                          .Include(x => x.TimeSlot.Business)
                                          .FirstOrDefaultAsync(x => x.TimeSlotId == timeSlotId
                                                                  && x.UserEmail == userEmail);
        }

        public async Task<IList<Booking>> FindBookingsByUser(string userEmail)
        {
            var bookings = await _context.Bookings.Include(x => x.TimeSlot)
                                          .Include(x => x.TimeSlot.Business)
                                          .Where(x => x.UserEmail.Equals(userEmail))
                                          .ToListAsync();
         

            return bookings;
        }

        public async Task<IList<Booking>> FindBookingsByBusiness(int businessId)
        {
            var bookings = await _context.Bookings.Include(x => x.TimeSlot)
                                          .Include(x => x.TimeSlot.Business)
                                          .Where(x => x.BusinessId == businessId)
                                          .ToListAsync();
    

            return bookings;
        }

        public async Task<HttpCode> DeleteBooking(string userEmail, int timeSlotId)
        {
            Booking booking = await FindBookingByUserAndTimeSlot(userEmail, timeSlotId);

            if (booking == null)
            {
                return HttpCode.NotFound;
            }

            _context.Bookings.Remove(booking);

            await _context.SaveChangesAsync();

            return HttpCode.Deleted;
        }
    }
}