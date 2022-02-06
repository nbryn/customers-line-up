using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using CLup.Domain.Bookings;
using MediatR;

namespace CLup.Application.EventHandlers.Booking
{
    public class UserDeletedBookingEventHandler : INotificationHandler<DomainEventNotification<UserDeletedBookingEvent>>
    {
        private readonly ICLupDbContext _context;

        public UserDeletedBookingEventHandler(ICLupDbContext context) => _context = context;

        public async Task Handle(
            DomainEventNotification<UserDeletedBookingEvent> @event,
            CancellationToken cancellationToken)
        {
            var domainEvent = @event.DomainEvent;
            var message =
                domainEvent.Booking.User.BookingDeletedMessage(domainEvent.Booking, domainEvent.Booking.BusinessId);

            await _context.Messages.AddAsync(message);
        }
    }
}