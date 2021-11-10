using System.Threading;
using System.Threading.Tasks;
using CLup.Domain.Booking;
using CLup.Domain.Message;
using CLup.Infrastructure;
using MediatR;

namespace CLup.Application.EventHandlers.Booking
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