using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.Bookings;
using Logic.Context;

namespace Data
{
    public interface IBookingRepository
    {
        Task<Response> DeleteBooking(string userEmail, int timeSlotId);
        Task<IList<Booking>> FindBookingsByUser(string userEmail);
        Task<(string, int)> SaveBooking(Booking booking);
        Task<Booking> FindBookingById(string email, int timeSlotId);
        Task<IList<Booking>> FindBookingsByBusiness(int businessId);

    }
}