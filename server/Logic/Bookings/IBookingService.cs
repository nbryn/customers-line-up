using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.Context;
using Logic.DTO;

namespace Logic.Bookings
{
    public interface IBookingService
    {
        Task<QueryResult> VerifyNewBooking(string userEmail, int timeSlotId);

        Task<HttpCode> VerifyDeleteBookingRequest(string ownerEmail, string userEmail, int timeSlotId);
    }
}