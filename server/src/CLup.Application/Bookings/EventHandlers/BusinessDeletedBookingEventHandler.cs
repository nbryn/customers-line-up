using System.Threading;
using System.Threading.Tasks;
using CLup.Data;
using CLup.Domain.Bookings;
using CLup.Domain.Messages;
using MediatR;

namespace CLup.Application.Bookings.EventHandlers
{
    public class BusinessDeletedBookingEventHandler : INotificationHandler<BusinessDeletedBookingEvent>
    {
        private readonly CLupContext _context;

        public BusinessDeletedBookingEventHandler(CLupContext context) => _context = context;

        public async Task Handle(BusinessDeletedBookingEvent @event, CancellationToken cancellationToken)
        {
            var message = MessageFactory.BookingDeletedMessage(@event.Booking.Business, @event.Booking.UserId);

            await _context.AddAsync(message);
        }
    }
}