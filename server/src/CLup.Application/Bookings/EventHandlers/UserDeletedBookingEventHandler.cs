using System.Threading;
using System.Threading.Tasks;
using CLup.Data;
using CLup.Domain.Bookings;
using CLup.Domain.Messages;
using MediatR;

namespace CLup.Application.Bookings.EventHandlers
{
    public class UserDeletedBookingEventHandler : INotificationHandler<UserDeletedBookingEvent>
    {
        private readonly CLupContext _context;

        public UserDeletedBookingEventHandler(CLupContext context) => _context = context;

        public async Task Handle(UserDeletedBookingEvent @event, CancellationToken cancellationToken)
        {
            var message = MessageFactory.BookingDeletedMessage(@event.Booking, @event.Booking.BusinessId);

            await _context.AddAsync(message);
        }
    }
}