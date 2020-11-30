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

        public async Task<Booking> FindBookingById(string email, int timeSlotId)
        {
            return await _context.Bookings.FirstOrDefaultAsync(x => x.UserEmail.Equals(email) && 
                                                              x.TimeSlotId == timeSlotId);
        }

        public async Task<IList<Booking>> FindBookingsByUser(string userEmail)
        {
            return await _context.Bookings.Include(x => x.TimeSlot)
                                            .Where(x => x.UserEmail.Equals(userEmail)).ToListAsync();
        }

        public async Task<int> DeleteBooking(string userEmail, int timeSlotId)
        {
            Booking booking = await _context.Bookings.FirstOrDefaultAsync(b => b.UserEmail.Equals(userEmail)
                                                                          && b.TimeSlotId == timeSlotId);
            if (booking == null)
            {
                return 
            }

            _context.Bookings.Remove(booking);

            await _context.SaveChangesAsync();
        }
    }
}