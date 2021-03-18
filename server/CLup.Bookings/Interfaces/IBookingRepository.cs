using System.Collections.Generic;
using System.Threading.Tasks;

using CLup.Bookings.DTO;
using CLup.Context;
using CLup.Util;

namespace CLup.Bookings.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<ServiceResponse> DeleteBooking(string userEmail, int timeSlotId);
        Task<ServiceResponse<IList<BookingDTO>>> FindBookingsByUser(string userEmail);
        Task<ServiceResponse> SaveBooking(Booking booking);
        Task<Booking> FindBookingByUserAndTimeSlot(string userEmail, int timeSlotId);
        Task<ServiceResponse<IList<BookingDTO>>> FindBookingsByBusiness(int businessId);

    }
}