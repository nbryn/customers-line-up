using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.Bookings;

namespace Data
{
    public interface IBookingRepository
    {

        Task DeleteBooking(string userEmail, int timeSlotId);
        Task<IList<Booking>> FindBookingsByUser(string userEmail);
        Task<(string, int)> SaveBooking(Booking booking);
        Task<Booking> FindBookingByEmail(string email);

    }
}