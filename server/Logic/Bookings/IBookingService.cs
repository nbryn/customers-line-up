using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.Context;
using Logic.DTO;

namespace Logic.Bookings
{
    public interface IBookingService
    {
        Task<(Response, string?)> CreateBooking(string userEmail, int timeSlotId);

        Task<Response> VerifyDeleteBookingRequest(string ownerEmail, string userEmail, int timeSlotId);
    }
}