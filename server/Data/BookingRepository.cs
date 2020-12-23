using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Logic.Bookings;
using Logic.Context;

namespace Data
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ICLupContext _context;

        public BookingRepository(ICLupContext context)
        {
            _context = context;
        }
        public async Task<(string, int)> SaveBooking(Booking booking)
        {
            _context.Bookings.Add(booking);

            await _context.SaveChangesAsync();

            return (booking.UserEmail, booking.TimeSlotId);
        }

        public async Task<Booking> FindBookingByUser(string userEmail, int timeSlotId)
        {
            return await _context.Bookings.Include(x => x.TimeSlot)
                                          .FirstOrDefaultAsync(x => x.TimeSlotId == timeSlotId
                                                                  && x.UserEmail == userEmail);
        }

        public async Task<IList<Booking>> FindBookingsByUser(string userEmail)
        {
            return await _context.Bookings.Include(x => x.TimeSlot)
                                          .Where(x => x.UserEmail.Equals(userEmail)).ToListAsync();
        }

        public async Task<IList<Booking>> FindBookingsByBusiness(int businessId)
        {
            return await _context.Bookings.Include(x => x.TimeSlot)
                                          .Where(x => x.BusinessId == businessId).ToListAsync();
        }

        public async Task<Response> DeleteBooking(string userEmail, int timeSlotId)
        {
            Booking booking = await FindBookingByUser(userEmail, timeSlotId);
            if (booking == null)
            {
                return Response.NotFound;
            }

            _context.Bookings.Remove(booking);

            await _context.SaveChangesAsync();

            return Response.Deleted;
        }
    }
}