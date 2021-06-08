using System.Collections.Generic;
using System.Threading.Tasks;

using CLup.Bookings.DTO;
using CLup.Context;
using CLup.Util;

namespace CLup.Bookings.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<ServiceResponse> DeleteBooking(string userEmail, string timeSlotId);
        Task<ServiceResponse<IList<BookingDTO>>> FindBookingsByUser(string userEmail);
        Task<ServiceResponse> SaveBooking(Booking booking);
        Task<Booking> FindBookingByUserAndTimeSlot(string userEmail, string timeSlotId);
        Task<Booking> FindNextBookingByUser(string userEmail);
        Task<ServiceResponse<IList<BookingDTO>>> FindBookingsByBusiness(string businessId);

    }
}