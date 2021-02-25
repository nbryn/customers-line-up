using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.Bookings;
using Logic.DTO;

namespace Data
{
    public interface IBookingRepository
    {
        Task<HttpCode> DeleteBooking(string userEmail, int timeSlotId);
        Task<IList<Booking>> FindBookingsByUser(string userEmail);
        Task<HttpCode> SaveBooking(Booking booking);
        Task<Booking> FindBookingByUserAndTimeSlot(string userEmail, int timeSlotId);
        Task<IList<Booking>> FindBookingsByBusiness(int businessId);

    }
}