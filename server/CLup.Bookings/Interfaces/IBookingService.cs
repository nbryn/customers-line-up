using System.Threading.Tasks;

using CLup.Util;

namespace CLup.Bookings.Interfaces
{
    public interface IBookingService
    {
        Task<ServiceResponse> VerifyNewBooking(string userEmail, string timeSlotId);

        Task<ServiceResponse> VerifyDeleteBookingRequest(string ownerEmail, string userEmail, string timeSlotId);
    }
}