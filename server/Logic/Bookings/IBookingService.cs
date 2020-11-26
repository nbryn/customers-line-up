using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Bookings
{
    public interface IBookingService
    {
        Task<int> CreateBooking(string userEmail, int timeSlotId);
    }
}