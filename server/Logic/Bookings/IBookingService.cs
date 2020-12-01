using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.Context;

namespace Logic.Bookings
{
    public interface IBookingService
    {
        Task<(Response, string?)> CreateBooking(string userEmail, int timeSlotId);
    }
}